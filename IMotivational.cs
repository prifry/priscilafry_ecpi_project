using System ;
using System.Data.SQLite;
using System.Collections.Generic;
/****************************************************************************************************
* Priscila Fry
* CIS317
* Date: 11/20/2023
* Project description:  This code defines an interface class, IMotivational, which declares a method
*   for displaying motivational quotes. The interface is intended to be implemented
*   by classes that provide motivational functionality in a larger application.
*
******************************************************************************************************/

// Interface class for objects that provide motivational quotes
public interface IMotivational
{
    void DisplayMotivationalQuote();

}