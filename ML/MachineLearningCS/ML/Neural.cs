using System;
using System.Text;
using System.IO;
using System.Linq;
using Accord.Neuro;
using Accord.Neuro.Networks;
using Accord.Neuro.Learning;
using AForge.Neuro.Learning;
using Accord.Neuro.ActivationFunctions;
using Accord.Math;


namespace ML
{
    class Neuro
    {
        public double[][] inputs { get; set; }
        public double[][] outputs { get; set; }
        public DeepBeliefNetwork network;

        public Neuro(double[][] input, double[][] output)
        {
            this.inputs = input;
            this.outputs = output;
        }

        public Neuro() { }

        public void train()
        {
            network = new DeepBeliefNetwork(inputsCount: inputs.Length,
                                                hiddenNeurons: new int[] { 4, outputs[0].Length }); // 隠れ層と出力層の次元
            // DNNの学習アルゴリズムの生成
            var teacher = new DeepNeuralNetworkLearning(network)
            {
                Algorithm = (ann, i) => new ParallelResilientBackpropagationLearning(ann),
                LayerIndex = network.Machines.Count - 1
            };

            // 5000回学習
            var layerData = teacher.GetLayerInput(inputs);
            for (int i = 0; i < 5000; i++)
            {
                teacher.RunEpoch(layerData, outputs);
            }

            // 重みの更新
            network.UpdateVisibleWeights();
        }

        public void predict(double[] test)
        {
            var pre = network.Compute(test);
            int imax;
            pre.Max(out imax);
            Console.WriteLine("class : {0}", imax);
            foreach (var item in pre)
            {
                Console.WriteLine("{0}", item);
            }
        }
    }
}
