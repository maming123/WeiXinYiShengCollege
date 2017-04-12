using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Module.Utils
{
   public class GenerateSequence
    {
       
       public static List<int> GetRandomSequence(int low, int high)
       {
           int[] arr = BuildRandomSequence4(low, high);
           List<int> list = new List<int>();
           for(int i=0;i<arr.Length;i++)
           {
               list.Add(arr[i]);
           }
           return list;
       }
        /// <summary>
        /// 生成一个非重复的随机序列。速度最快
        /// </summary>
        /// <param name="low">序列最小值。</param>
        /// <param name="high">序列最大值。</param>
        /// <returns>序列。</returns>
        private static int[] BuildRandomSequence4(int low, int high)
        {
            int x = 0, tmp = 0;
            if (low > high)
            {
                tmp = low;
                low = high;
                high = tmp;
            }
            int[] array = new int[high - low + 1];
            for (int i = low; i <= high; i++)
            {
                array[i - low] = i;
            }
            Random random = new Random();
            for (int i = array.Length - 1; i > 0; i--)
            {
                x = random.Next(0, i + 1);
                tmp = array[i];
                array[i] = array[x];
                array[x] = tmp;
            }
            return array;
        }

        /// <summary>
        /// 生成一个非重复的随机序列。
        /// </summary>
        /// <param name="count">序列长度。</param>
        /// <returns>序列。</returns>
        private static int[] BuildRandomSequence3(int length)
        {
            int[] array = new int[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
            int x = 0, tmp = 0;
            Random random = new Random();

            for (int i = array.Length - 1; i > 0; i--)
            {
                x = random.Next(0, i + 1);
                tmp = array[i];
                array[i] = array[x];
                array[x] = tmp;
            }
            return array;
        }

    }
}
