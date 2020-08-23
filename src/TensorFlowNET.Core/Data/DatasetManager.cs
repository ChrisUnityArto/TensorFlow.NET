﻿using NumSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Tensorflow.Data;

namespace Tensorflow
{
    public class DatasetManager
    {
        public IDatasetV2 from_generator<T>(IEnumerable<T> generator, TF_DataType[] output_types, TensorShape[] output_shapes)
            => new GeneratorDataset();

        /// <summary>
        /// Creates a `Dataset` with a single element, comprising the given tensors.
        /// </summary>
        /// <param name="tensors"></param>
        /// <returns></returns>
        public IDatasetV2 from_tensor(NDArray tensors)
            => new TensorDataset(tensors);

        public IDatasetV2 from_tensor(Tensor tensors)
            => new TensorDataset(tensors);

        public IDatasetV2 from_tensor_slices(Tensor features, Tensor labels)
            => new TensorSliceDataset(features, labels);

        public IDatasetV2 range(int count, TF_DataType output_type = TF_DataType.TF_INT64)
            => new RangeDataset(count, output_type: output_type);

        public IDatasetV2 range(int start, int stop, int step = 1, TF_DataType output_type = TF_DataType.TF_INT64)
            => new RangeDataset(stop, start: start, step: step, output_type: output_type);
    }
}
