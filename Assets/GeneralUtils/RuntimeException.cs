﻿using System;

namespace Helper.ExtendSpace
{

    /// <summary>
    /// An unexpected Exception that occurs during execution.
    /// </summary>
    public class RuntimeException : Exception
    {

        /// <summary>
        /// Construct a brand new <see name="RuntimeException"/>.
        /// </summary>
        /// <param name="errorMessage">The error message to initialize the RuntimeException with.</param>
        public RuntimeException(string errorMessage) : base(errorMessage)
        {
        }

    }

}