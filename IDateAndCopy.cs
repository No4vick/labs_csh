using System;

namespace lab3sh
{
    interface IDateAndCopy
    {
        object DeepCopy(); 
        DateTime Date { get; set; }
    }
}
