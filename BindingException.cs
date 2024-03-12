using System;

namespace WinFormsApplab3
{
    public class BindingException : Exception
    {
        public BindingException(string field, string message) : base(message)
        {
            ErrorField = field;
        }

        public string ErrorField { get; set; }
    }
}