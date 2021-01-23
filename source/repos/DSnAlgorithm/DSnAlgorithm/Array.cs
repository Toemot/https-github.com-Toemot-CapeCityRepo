using System;

namespace DSnAlgorithm
{
    public class Array 
    {
        private int[] items;
        private int count;

        public Array(int size)
        {
            items = new int[size];
        }

        public void Insert(int item) 
        {
            //if array is full resize it
            if(items.Length == count)
            {
                //create a new array twice the size
                int[] newItems = new int[count * 2];
                //copy all the exisiting items to the new array
                for (int i = 0; i < count; i++)
                    newItems[i] = items[i];
                //set items to the new array
                items = newItems;
            }
            items[count++] = item;
        }

        public void Print() 
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(items[i]);
            }
            Console.ReadLine();
        }

        public void RemoveAt(int item) 
        {
            items[--count] = item;
        }
    }
}
