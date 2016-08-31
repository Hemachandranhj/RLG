using System;
using Encog.App.Analyst;
using Encog.App.Analyst.CSV.Normalize;
using Encog.App.Analyst.CSV.Segregate;
using Encog.App.Analyst.CSV.Shuffle;
using Encog.App.Analyst.Wizard;
using Encog.Engine.Network.Activation;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Encog.Persist;
using Encog.Util.CSV;
using Encog.Util.File;
using Encog.Util.Simple;
using System.IO;

namespace Carreer
{
    public class CareerRecommendation
    {
        public string GetNonAcademyCareerOption()
        {
            // Step 1 shuffle the data
            Shuffle(InterestConstants.BaseFile, InterestConstants.ShuffledBaseFile);
            // Step 2 Segregate data for evaluation
            Segregate(InterestConstants.BaseFile, InterestConstants.TrainingFile, InterestConstants.EvaluateWasteFile);
            // Step 3 Normalize the data
            Normalize(InterestConstants.BaseFile, InterestConstants.TrainingFile, InterestConstants.NormalizedTrainingFile, InterestConstants.EvaluateFile, InterestConstants.NormalizedEvaluateFile, InterestConstants.AnalystFile);
            // Step 4 Create Network
            CreateNetwork(InterestConstants.TrainedNetworkFile);
            // Step 5 TrainNetwork
            TrainNetwork(InterestConstants.TrainedNetworkFile, InterestConstants.NormalizedTrainingFile);

            return Evaluate(InterestConstants.TrainedNetworkFile, InterestConstants.AnalystFile, InterestConstants.NormalizedEvaluateFile);
        }

        public string GetAcademicOptions()
        {
            // Step 1 shuffle the data
            Shuffle(AcademicConstants.BaseFile, AcademicConstants.ShuffledBaseFile);
            // Step 2 Segregate data for evaluation
            Segregate(AcademicConstants.BaseFile, AcademicConstants.TrainingFile, AcademicConstants.EvaluateWasteFile);
            // Step 3 Normalize the data
            Normalize(AcademicConstants.BaseFile, AcademicConstants.TrainingFile, AcademicConstants.NormalizedTrainingFile, AcademicConstants.EvaluateFile, AcademicConstants.NormalizedEvaluateFile, AcademicConstants.AnalystFile);
            // Step 4 Create Network
            CreateNetwork(AcademicConstants.TrainedNetworkFile);
            // Step 5 TrainNetwork
            TrainNetwork(AcademicConstants.TrainedNetworkFile, AcademicConstants.NormalizedTrainingFile);

            return Evaluate(AcademicConstants.TrainedNetworkFile, AcademicConstants.AnalystFile, AcademicConstants.NormalizedEvaluateFile);
        }

        private void Shuffle(FileInfo source, FileInfo shuffledBaseFile)
        {
            var shuffle = new ShuffleCSV();
            shuffle.Analyze(source, true, CSVFormat.English);
            shuffle.ProduceOutputHeaders = true;
            shuffle.Process(shuffledBaseFile);
        }

        private void Segregate(FileInfo source, FileInfo TrainingFile, FileInfo EvaluateWasteFile)
        {
            //Segregate source file into training and evaluation file
            var seg = new SegregateCSV();
            seg.Targets.Add(new SegregateTargetPercent(TrainingFile, 100));
            seg.Targets.Add(new SegregateTargetPercent(EvaluateWasteFile, 0));
            seg.ProduceOutputHeaders = true;
            seg.Analyze(source, true, CSVFormat.English);
            seg.Process();
        }

        private void Normalize( FileInfo BaseFile, FileInfo TrainingFile, FileInfo NormalizedTrainingFile, FileInfo EvaluateFile, FileInfo NormalizedEvaluateFile, FileInfo AnalystFile)
        {
            //Analyst
            var analyst = new EncogAnalyst();
            //Wizard
            var wizard = new AnalystWizard(analyst);
            wizard.Wizard(BaseFile, true, AnalystFileFormat.DecpntComma);

            //Norm for Trainng
            var norm = new AnalystNormalizeCSV();
            norm.Analyze(TrainingFile, true, CSVFormat.English, analyst);
            norm.ProduceOutputHeaders = true;
            norm.Normalize(NormalizedTrainingFile);

            //Norm of evaluation
            norm.Analyze(EvaluateFile, true, CSVFormat.English, analyst);
            norm.Normalize(NormalizedEvaluateFile);

            //save the analyst file
            analyst.Save(AnalystFile);
        }

        public void CreateNetwork(FileInfo networkFile)
        {
            var network = new BasicNetwork();
            network.AddLayer(new BasicLayer(new ActivationLinear(), true, 3));
            network.AddLayer(new BasicLayer(new ActivationTANH(), true, 6));
            network.AddLayer(new BasicLayer(new ActivationTANH(), false, 1));
            network.Structure.FinalizeStructure();
            network.Reset();
            EncogDirectoryPersistence.SaveObject(networkFile, (BasicNetwork)network);
        }

        public void TrainNetwork(FileInfo TrainedNetworkFile, FileInfo NormalizedTrainingFile)
        {
            var network = (BasicNetwork)EncogDirectoryPersistence.LoadObject(TrainedNetworkFile);
            var trainingSet = EncogUtility.LoadCSV2Memory(NormalizedTrainingFile.ToString(),
                network.InputCount, network.OutputCount, true, CSVFormat.English, false);


            var train = new ResilientPropagation(network, trainingSet);
            int epoch = 1;
            do
            {
                train.Iteration();
                Console.WriteLine("Epoch : {0} Error : {1}", epoch, train.Error);
                epoch++;
            } while (train.Error > 0.01);

            EncogDirectoryPersistence.SaveObject(TrainedNetworkFile, (BasicNetwork)network);
        }

        public string Evaluate(FileInfo TrainedNetworkFile, FileInfo AnalystFile,FileInfo NormalizedEvaluateFile)
        {
            var network = (BasicNetwork)EncogDirectoryPersistence.LoadObject(TrainedNetworkFile);
            var analyst = new EncogAnalyst();
            analyst.Load(AnalystFile.ToString());
            var evaluationSet = EncogUtility.LoadCSV2Memory(NormalizedEvaluateFile.ToString(),
                network.InputCount, network.OutputCount, true, CSVFormat.English, false);
            var career = string.Empty;

            foreach (var item in evaluationSet)
            {
                var output = network.Compute(item.Input);

                int classCount = analyst.Script.Normalize.NormalizedFields[3].Classes.Count;
                double normalizationHigh = analyst.Script.Normalize.NormalizedFields[3].NormalizedHigh;
                double normalizationLow = analyst.Script.Normalize.NormalizedFields[3].NormalizedLow;

                var eq = new Encog.MathUtil.Equilateral(classCount, normalizationHigh, normalizationLow);
                var predictedClassInt = eq.Decode(output);

                career = analyst.Script.Normalize.NormalizedFields[3].Classes[predictedClassInt].Name;
            }

            return career;
        }

    }

    public static class InterestConstants
    {
        public static FileInfo BasePath = new FileInfo(@"C:\Users\user\Desktop\CareerEngine\CareerEngine\Data   ");
        public static FileInfo ShuffledBaseFile = FileUtil.CombinePath(BasePath, "InterestData_Shuffled.csv");
        public static FileInfo BaseFile = FileUtil.CombinePath(BasePath, "InterestData.csv");
        public static FileInfo TrainingFile = FileUtil.CombinePath(BasePath, "InterestData_Train.csv");
        public static FileInfo EvaluateFile = FileUtil.CombinePath(BasePath, "InterestData_Eval.csv");
        public static FileInfo EvaluateWasteFile = FileUtil.CombinePath(BasePath, "InterestData_waste.csv");
        public static FileInfo NormalizedTrainingFile = FileUtil.CombinePath(BasePath, "InterestData_Train_Norm.csv");
        public static FileInfo NormalizedEvaluateFile = FileUtil.CombinePath(BasePath, "InterestData_Eval_Norm.csv");
        public static FileInfo AnalystFile = FileUtil.CombinePath(BasePath, "InterestData_Analyst.ega");
        public static FileInfo TrainedNetworkFile = FileUtil.CombinePath(BasePath, "InterestData_Train.eg");
    }

    public static class AcademicConstants
    {
        public static FileInfo BasePath = new FileInfo(@"C:\Users\user\Desktop\CareerEngine\CareerEngine\Data   ");
        public static FileInfo ShuffledBaseFile = FileUtil.CombinePath(BasePath, "Academic_Shuffled.csv");
        public static FileInfo BaseFile = FileUtil.CombinePath(BasePath, "AcademicData.csv");
        public static FileInfo TrainingFile = FileUtil.CombinePath(BasePath, "AcademicData_Train.csv");
        public static FileInfo EvaluateFile = FileUtil.CombinePath(BasePath, "AcademicData_Eval.csv");
        public static FileInfo EvaluateWasteFile = FileUtil.CombinePath(BasePath, "AcademicData_waste.csv");
        public static FileInfo NormalizedTrainingFile = FileUtil.CombinePath(BasePath, "AcademicData_Train_Norm.csv");
        public static FileInfo NormalizedEvaluateFile = FileUtil.CombinePath(BasePath, "AcademicData_Eval_Norm.csv");
        public static FileInfo AnalystFile = FileUtil.CombinePath(BasePath, "AcademicData_Analyst.ega");
        public static FileInfo TrainedNetworkFile = FileUtil.CombinePath(BasePath, "AcademicData_Train.eg");
    }
}
