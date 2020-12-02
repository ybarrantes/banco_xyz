﻿using System;

namespace BancoXYZ.Business.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException() { }
        public RepositoryException(string message)
            : base(message)
        { }
        public RepositoryException(string message, Exception innerException)
            :base(message, innerException)
        { }
    }
}
