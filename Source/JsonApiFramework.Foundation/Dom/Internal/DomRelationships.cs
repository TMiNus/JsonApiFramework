// Copyright (c) 2015�Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace JsonApiFramework.Dom.Internal
{
    internal class DomRelationships : DomObject
        , IDomRelationships
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public DomRelationships(params DomProperty[] domProperties)
            : this(domProperties.AsEnumerable())
        { }

        public DomRelationships(IEnumerable<DomProperty> domProperties)
            : base("relationships object", domProperties)
        { }
        #endregion
    }
}
