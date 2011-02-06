﻿using System;
using Whitebox.Model;

namespace Whitebox.Messages
{
    [Serializable]
    public class TypeDiscoveredMessage
    {
        readonly TypeModel _typeModel;

        public TypeDiscoveredMessage(TypeModel typeModel)
        {
            if (typeModel == null) throw new ArgumentNullException("typeModel");
            _typeModel = typeModel;
        }

        public TypeModel TypeModel
        {
            get { return _typeModel; }
        }
    }
}
