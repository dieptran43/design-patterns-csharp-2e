﻿using System;
using System.Collections.Generic;

namespace VisitorPattern
{
    /// <summary>
    /// Abstract class- Number
    /// </summary>
    abstract class Number
    {
        private int numberValue;
        private string type;
        public Number(string type, int number)
        {
            this.type = type;
            this.numberValue = number;
        }
        //I want to restrict the change in original data
        //So, no setter is present here.
        public int NumberValue
        {
            get
            {
                return numberValue;
            }
            //I want to restrict the change in original data
            //set
            //{
            //    numberValue = value;
            //}
        }
        public string TypeInfo
        {
            get
            {
                return type;
            }
        }
        public abstract void Accept(IVisitor visitor);
    }
    /// <summary>
    /// Concrete class-SmallNumber
    /// </summary>

    class SmallNumber : Number
    {
        public SmallNumber(string type, int number) : base(type, number)
        { }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitSmallNumbers(this);
        }
    }
    /// <summary>
    /// Concrete class-BigNumber
    /// </summary>
    class BigNumber : Number
    {
        public BigNumber(string type, int number) : base(type, number)
        { }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitBigNumbers(this);
        }
    }
    class NumberCollection
    {
        List<Number> numberList = new List<Number>();
        //List contains both SmallNumber's and BigNumber's
        public NumberCollection()
        {
            numberList.Add(new SmallNumber("small-1", 10));
            numberList.Add(new SmallNumber("small-2", 20));
            numberList.Add(new SmallNumber("small-3", 30));
            numberList.Add(new BigNumber("big-1", 200));
            numberList.Add(new BigNumber("big-2", 150));
            numberList.Add(new BigNumber("big-3", 70));
        }
        public void AddNumberToList(Number number)
        {
            numberList.Add(number);
        }
        public void RemoveNumberFromList(Number number)
        {
            numberList.Remove(number);
        }
        public void DisplayList()
        {
            Console.WriteLine("Current list is as follows:");
            foreach (Number number in numberList)
            {
                Console.Write(number.NumberValue + "\t");
            }
            Console.WriteLine();
        }
        public void Accept(IVisitor visitor)
        {
            foreach (Number n in numberList)
            {
                n.Accept(visitor);
            }
        }
    }
    /// <summary>
    /// The Visitor interface.
    /// GoF suggests to make visit opearation for each concrete class of ConcreteElement
    /// (in our example,SmallNumber and BigNumber) in the object structure
    /// </summary>
    interface IVisitor
    {
        //A visit operation for SmallNumber class
        void VisitSmallNumbers(SmallNumber number);

        //A visit operation for BigNumber class
        void VisitBigNumbers(BigNumber number);
    }
    /// <summary>
    /// A concrete visitor-IncrementNumberVisitor 
    /// </summary>
    class IncrementNumberVisitor : IVisitor
    {
        public void VisitSmallNumbers(SmallNumber number)
        {
            Number currentNumber = number as Number;
            /* 
             I do not want( infact I can't change because it's readonly now) to modify the original data. So, I'm making a copy of it before I use it.
            */
            int temp = currentNumber.NumberValue;
            //For SmallNumber's incrementing by 1
            Console.WriteLine($"{currentNumber.TypeInfo} is {currentNumber.NumberValue}; I use it as:{++temp} for rest of my code.");
            //Remaining code, if any
        }

        public void VisitBigNumbers(BigNumber number)
        {
            Number currentNumber = number as Number;
            /* 
             I do not want( infact I can't change because it's readonly now) to modify the original data. So, I'm making a copy of it before I use it.
            */
            int temp = currentNumber.NumberValue;
            //For BigNumber's incrementing by 10
            Console.WriteLine($"{currentNumber.TypeInfo} is {currentNumber.NumberValue}; I convert it as:{temp + 10} for rest of my code.");
            //Remaining code, if any
        }
    }
    /// <summary>
    /// Client
    /// </summary>
    class Client
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***Visitor Pattern Demo***\n");
            NumberCollection numberCollection = new NumberCollection();
            //Showing the current list
            numberCollection.DisplayList();
            IncrementNumberVisitor incrVisitor = new IncrementNumberVisitor();
            //Visitor is visiting the list
            Console.WriteLine("IncrementNumberVisitor is about to visit the list:");
            numberCollection.Accept(incrVisitor);
            //Showing the current list
            numberCollection.DisplayList();

            Console.ReadLine();
        }
    }
}
