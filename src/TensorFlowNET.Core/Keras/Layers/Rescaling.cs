﻿using System;
using System.Collections.Generic;
using System.Text;
using Tensorflow.Keras.ArgsDefinition;
using Tensorflow.Keras.Engine;

namespace Tensorflow.Keras.Layers
{
    /// <summary>
    /// Multiply inputs by `scale` and adds `offset`.
    /// </summary>
    public class Rescaling : Layer
    {
        RescalingArgs args;
        Tensor scale;
        Tensor offset;

        public Rescaling(RescalingArgs args) : base(args)
        {
            this.args = args;
        }

        protected override Tensors call(Tensors inputs, Tensor state = null, bool is_training = false)
        {
            scale = math_ops.cast(args.Scale, args.DType);
            offset = math_ops.cast(args.Offset, args.DType);
            return math_ops.cast(inputs, args.DType) * scale + offset;
        }
    }
}
