﻿using System;
using System.Text;
using System.IO;
using System.Linq;
using Accord.MachineLearning.VectorMachines;
using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math;
using Accord.IO;


// http://accord-framework.net/docs/html/N_Accord_MachineLearning_VectorMachines.htm
namespace ML
{
    class SVM
    {
        MulticlassSupportVectorMachine msvm;
        double[][] inputs { get; set; }
        int[] outputs { get; set; }

        public SVM(double[][] inputs, int[] outputs)
        {
            this.inputs = inputs;
            this.outputs = outputs;
        }

        public SVM() { }

        public void learn()
        {
            var kernel = new Accord.Statistics.Kernels.Linear();
            var classes = outputs.GroupBy(x => x).Count();
            msvm = new MulticlassSupportVectorMachine(0, kernel, classes);
            var teacher = new MulticlassSupportVectorLearning(msvm, inputs, outputs);
            teacher.Algorithm = (machine, inputs, outputs, class1, class2) =>
            {
                var smo = new SequentialMinimalOptimization(machine, inputs, outputs);
                smo.UseComplexityHeuristic = true;
                return smo;
            };
            teacher.Run();
        }
        public int predict(double[] data)
        {
            var result = msvm.Compute(data);
            return result;
        }
    }
}