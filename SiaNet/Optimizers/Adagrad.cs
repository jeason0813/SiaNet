﻿using System;
using System.Collections.Generic;
using System.Text;
using SiaNet.Engine;
using SiaNet.Layers;

namespace SiaNet.Optimizers
{
    public class Adagrad : BaseOptimizer
    {
        public float Epsilon { get; set; }

        private Dictionary<string, Tensor> accumulators;

        public Adagrad(float lr = 0.01f, float decayRate = 0, float epsilon = 1e-07f)
            : base(lr, "adagrad")
        {
            DecayRate = decayRate;
            Epsilon = epsilon;
            accumulators = new Dictionary<string, Tensor>();
        }

        public override void Update(int iteration, BaseLayer layer)
        {
            if (DecayRate > 0)
            {
                LearningRate = LearningRate * (1 / 1 + DecayRate * iteration);
            }

            foreach (var item in layer.Params)
            {
                var param = item.Value;
                if (!accumulators.ContainsKey(param.Name))
                {
                    accumulators[param.Name] = K.Constant(0, param.Data.Shape);
                }

                accumulators[param.Name] = accumulators[param.Name] + K.Square(param.Grad);
                param.Data = param.Data - (LearningRate * param.Grad / (K.Sqrt(accumulators[param.Name]) + K.Epsilon()));

                param.ApplyConstraint();
            }
        }
    }
}
