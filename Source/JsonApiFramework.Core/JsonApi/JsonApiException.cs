﻿// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Net;

namespace JsonApiFramework.JsonApi
{
    /// <summary>
    /// Represents an unexpected exception with details encapsulated by one
    /// or more json:api Error objects.
    /// </summary>
    public class JsonApiException : Exception
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public JsonApiException(HttpStatusCode statusCode, Error error)
            : this(statusCode, new []{ error }, null)
        { }

        public JsonApiException(HttpStatusCode statusCode, Error error, Exception innerException)
            : this(statusCode, new []{ error }, innerException)
        { }

        public JsonApiException(HttpStatusCode statusCode, IEnumerable<Error> errors)
            : this(statusCode, errors, null)
        { }

        public JsonApiException(HttpStatusCode statusCode, IEnumerable<Error> errors, Exception innerException)
            : base("One or more errors has occurred. See the Errors property for details.", innerException)
        {
            this.StatusCode = statusCode;
            this.Errors = errors;
        }
        #endregion

        // PUBLIC PROPERTIES ////////////////////////////////////////////////
        #region Properties
        /// <summary>Represents the overall status code for the collection of errors as a whole.</summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>Represents the one or more actual errors that occurred during application execution.</summary>
        public IEnumerable<Error> Errors { get; }
        #endregion
    }
}