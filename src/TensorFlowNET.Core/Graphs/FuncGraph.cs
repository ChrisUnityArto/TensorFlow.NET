﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Tensorflow.Functions;
using static Tensorflow.Binding;

namespace Tensorflow.Graphs
{
    /// <summary>
    /// Graph representing a function body.
    /// </summary>
    public class FuncGraph : Graph
    {
        List<Operation> inputs;
        List<Operation> outputs;
        Graph outer_graph;
        string func_name;
        IntPtr func_handle;
        public string FuncName => c_api.StringPiece(c_api.TF_FunctionName(func_handle));

        /// <summary>
        /// Construct a new FuncGraph.
        /// </summary>
        public FuncGraph(string name) : base()
        {
            outer_graph = ops.get_default_graph();
            func_name = name;
        }

        public IntPtr ToGraph(Operation[] opers,
            Operation[] inputs, Operation[] outputs,
            string[] output_names)
        {
            using var status = new Status();
            func_handle = c_api.TF_GraphToFunction(_handle, 
                func_name, 
                false,
                opers.Length,
                opers.Select(x => (IntPtr)x).ToArray(),
                inputs.Length, 
                inputs.Select(x => new TF_Output(x, 0)).ToArray(),
                outputs.Length, 
                outputs.Select(x => new TF_Output(x, 0)).ToArray(),
                output_names == null || output_names.Length == 0 ? null : output_names,
                IntPtr.Zero, 
                null, 
                status.Handle);
            status.Check(true);

            c_api.TF_GraphCopyFunction(outer_graph, func_handle, IntPtr.Zero, status.Handle);
            status.Check(true);

            c_api.TFE_ContextAddFunction(tf.Context.Handle, func_handle, status.Handle);
            status.Check(true);

            return func_handle;
        }
    }
}
