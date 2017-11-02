using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Models;//add
using System.IO;//add
using Common;//add
using System.ServiceModel.Web;//add

namespace WcfServiceLibrary1
{
    public class Algorithm : IAlgorithm
    {
        public List<string> Combination(string length, Stream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string content = reader.ReadToEnd();

            var result = new List<string>();
            int count = int.Parse(length);
            try
            {
                var list = JsonHelper.ParseFromJson<List<char>>(content);

                Action<string, string> myFunc = null;
                myFunc = (s, c) =>
                {
                    s = s + c;
                    if (s.Length == count)
                    {
                        result.Add(s);
                    }
                    else
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            myFunc(s, list[i].ToString());
                        }
                    }
                };

                myFunc("", "");
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
                //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, "input data invalid=>" + ex.Message);
                //result.IsSuccess = false;
                //result.Code = 12130;
                //result.Message = "input data invalid";
                //return result;
            }

            return result;
        }

        public List<string> Perm(string length, Stream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string content = reader.ReadToEnd();

            var result = new List<string>();
            int count = int.Parse(length);
            try
            {
                var list = JsonHelper.ParseFromJson<List<char>>(content);

                Action<string, string> myFunc = null;
                myFunc = (s, c) =>
                {
                    s = s + c;
                    if (s.Length == count)
                    {
                        result.Add(s);
                    }
                    else
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            if (!s.Contains(list[i]))
                            {
                                myFunc(s, list[i].ToString());
                            }
                        }
                    }
                };

                myFunc("", "");
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
                //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, "input data invalid=>" + ex.Message);
                //result.IsSuccess = false;
                //result.Code = 12130;
                //result.Message = "input data invalid";
                //return result;
            }

            return result;
        }

        public int[] Sort(Stream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string content = reader.ReadToEnd();

            int[] result = null;

            try
            {
                var _list = JsonHelper.ParseFromJson<List<int>>(content);
                var list = _list.ToArray();
                var slist = new int[list.Length];

                for (int i = 0; i < list.Length; i++)
                {
                    slist[i] = list[i];
                    for (int j = i; j > 0; j--)
                    {
                        if (slist[j] < slist[j - 1])
                        {
                            int temp = slist[j];
                            slist[j] = slist[j - 1];
                            slist[j - 1] = temp;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                result = slist;
            }
            catch (Exception ex)
            {
                //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, "input data invalid=>" + ex.Message);
                //result.IsSuccess = false;
                //result.Code = 12130;
                //result.Message = "input data invalid";
                //return result;
            }

            return result;
        }

        public List<string> DictionaryOrder(Stream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string content = reader.ReadToEnd();

            var result = new List<string>();
            try
            {
                var list = JsonHelper.ParseFromJson<List<char>>(content);

                Action<string, int> myFunc = null;
                myFunc = (s, i) =>
                {
                    s = s + list[i];
                    result.Add(s);
                    if (i + 1 < list.Count)
                    {
                        for (int j = i + 1; j < list.Count; j++)
                        {
                            myFunc(s, j);
                        }
                    }
                };

                for (int i = 0; i < list.Count; i++)
                {
                    myFunc("", i);
                }
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
                //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, "input data invalid=>" + ex.Message);
                //result.IsSuccess = false;
                //result.Code = 12130;
                //result.Message = "input data invalid";
                //return result;
            }

            return result;
        }

        public List<string> DictionaryOrder2(string length, Stream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string content = reader.ReadToEnd();

            var result = new List<string>();

            int count = int.Parse(length);

            try
            {
                var list = JsonHelper.ParseFromJson<List<char>>(content);

                Action<string, int> myFunc = null;
                myFunc = (s, i) =>
                {
                    s = s + list[i];
                    if (s.Length == count)
                    {
                        result.Add(s);
                    }
                    else
                    {
                        int _index = i + 1;
                        if (_index < list.Count)
                        {
                            for (int j = _index; j <= list.Count - count + s.Length; j++)
                            {
                                myFunc(s, j);
                            }
                        }
                    }
                };

                for (int i = 0; i <= list.Count - count; i++)
                {
                    myFunc("", i);
                }
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
                //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, "input data invalid=>" + ex.Message);
                //result.IsSuccess = false;
                //result.Code = 12130;
                //result.Message = "input data invalid";
                //return result;
            }

            return result;
        }

        public List<string> IntTake(string num)
        {
            int current = int.Parse(num);
            var numbersList = new List<List<int>>();

            int remainder;
            int quotient = Math.DivRem(current, 2, out remainder);

            for (int i = 1; i <= quotient; i++)
            {
                for (int j = 1; j * i < current; j++)
                {
                    var numbers = new List<int>();
                    for (int k = 0; k < j; k++)
                    {
                        numbers.Add(i);
                    }
                    numbers.Add(current - j * i);
                    numbersList.Add(numbers);
                }
            }

            var result = new List<string>();

            foreach (List<int> numbers in numbersList)
            {
                result.Add(String.Join("+", numbers));
            }

            return result;
        }

        public List<string> Hanoi(string num)
        {
            var result = new List<string>();

            int count = int.Parse(num);

            try
            {
                Action<int, string, string, string> myFunc = null;
                myFunc = (n, f, c, t) =>
                {
                    if (n > 1)
                    {
                        myFunc(n - 1, f, t, c);
                        myFunc(1, f, c, t);
                        myFunc(n - 1, c, f, t);
                    }
                    else
                    {
                        result.Add(string.Format("{0} to {1}", f, t));
                    }
                };

                myFunc(count, "A", "B", "C");
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
                //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, "input data invalid=>" + ex.Message);
                //result.IsSuccess = false;
                //result.Code = 12130;
                //result.Message = "input data invalid";
                //return result;
            }

            return result;
        }

        public string GetWord(string word)
        {
            return word;
        }

        public int[] QuickSort(Stream stream)
        {
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);

            string content = reader.ReadToEnd();

            int[] result = null;

            try
            {
                var _list = JsonHelper.ParseFromJson<List<int>>(content);

                Func<int, int, int> ascending = (a, b) =>
                {
                    return a - b;
                };

                Func<int, int, int> descending = (a, b) =>
                {
                    return b - a;
                };

                Action<int[], int, int> swap = (list, a, b) =>
                {
                    int temp = list[a];
                    list[a] = list[b];
                    list[b] = temp;
                };


                Func<int[], int, int, int, Func<int, int, int>, int> getFrom = null;
                getFrom = (list, val, from, to, c) =>
                {
                    int i = from;
                    while (i <= to && c(val, list[i]) >= 0)//care
                    {
                        i++;
                    }
                    return i;

                    //var i = from;
                    //while (i < to + 1 && c(list[i], val) <= 0) { i++; }
                    //return i;
                };

                Func<int[], int, int, int, Func<int, int, int>, int> getTo = null;
                getTo = (list, val, from, to, c) =>
                {
                    int i = from;
                    while (i >= to && c(val, list[i]) < 0)//care
                    {
                        i--;
                    }
                    return i;

                    //int j = from;
                    //while (j > to - 1 && c(list[j], val) > 0) { j--; }
                    //return j;
                };

                //cor
                Func<int[], int, int, int, Func<int, int, int>, int> partitionUnprocessed = null;
                partitionUnprocessed = (list, s, left, right, c) =>
                {
                    var i = getFrom(list, s, left, right, c);
                    var j = getTo(list, s, right, i, c);//care
                    if (i < j)
                    {
                        swap(list, i, j);
                        return partitionUnprocessed(list, s, i + 1, j - 1, c);
                    }
                    return j;
                };

                Func<int[], int, int, Func<int, int, int>, int> getAxis = null;
                getAxis = (list, left, right, c) =>
                {
                    var s = list[left];
                    var axis = partitionUnprocessed(list, s, left + 1, right, c);
                    swap(list, left, axis);
                    return axis;
                };

                Action<int[], int, int, Func<int, int, int>> quick = null;
                quick = (list, left, right, c) =>
                {
                    if (left < right)//care!!
                    {
                        var axis = getAxis(list, left, right, c);
                        quick(list, left, axis - 1, c);
                        quick(list, axis + 1, right, c);
                    }
                };

                result = _list.ToArray();
                quick(result, 0, _list.Count - 1, ascending);
                return result;
            }
            catch (Exception ex)
            {
                //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, "input data invalid=>" + ex.Message);
                //result.IsSuccess = false;
                //result.Code = 12130;
                //result.Message = "input data invalid";
                //return result;
            }

            return result;
        }

        //public int BinarySearch(string keyword, Stream stream)
        //{
        //    StreamReader reader = new StreamReader(stream, Encoding.UTF8);

        //    string content = reader.ReadToEnd();

        //    int result = 0;

        //    int key = int.Parse(keyword);

        //    try
        //    {
        //        var _list = JsonHelper.ParseFromJson<List<int>>(content);

        //        int _mid = _list.Count / 2 + 1;

        //        Func<int, int> getMid = null;
        //        getMid = (mid) =>
        //        {
        //            //if_list[mid]
        //            return 1;
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        //LogManager.WriteInvokeResult("OpRegisterVoipService", string.Empty, "input data invalid=>" + ex.Message);
        //        //result.IsSuccess = false;
        //        //result.Code = 12130;
        //        //result.Message = "input data invalid";
        //        //return result;
        //    }

        //    return result;
        //}
    }
}
