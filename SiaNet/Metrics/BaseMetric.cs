﻿using SiaNet.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace SiaNet.Metrics
{
    public abstract class BaseMetric
    {
        internal IBackend K = Global.CurrentBackend;

        public string Name { get; set; }

        public BaseMetric(string name)
        {
            Name = name;
        }

        public abstract Tensor Calc(Tensor Tensor, Tensor labels);

        public static BaseMetric Get(MetricType metricType)
        {
            BaseMetric metric = null;
            switch (metricType)
            {
                case MetricType.Accuracy:
                    metric = new Accuracy();
                    break;
                case MetricType.BinaryAccurary:
                    metric = new BinaryAccuracy();
                    break;
                case MetricType.MSE:
                    metric = new MSE();
                    break;
                case MetricType.MAE:
                    metric = new MAE();
                    break;
                case MetricType.MAPE:
                    metric = new MAPE();
                    break;
                case MetricType.MSLE:
                    metric = new MSLE();
                    break;
                default:
                    break;
            }

            return metric;
        }
    }
}
