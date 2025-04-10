﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            // поля
            private string _name;
            private string _surname;
            protected int[] _penaltytimes;

            // свойства
            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public int[] Penalties
            {
                get
                {
                    if (_penaltytimes == null) return null;
                    int[] results = new int[_penaltytimes.Length];
                    Array.Copy(_penaltytimes, results, results.Length);
                    return results;
                }
            }
            public int Total
            {
                get
                {
                    if (_penaltytimes == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        sum += _penaltytimes[i];
                    }
                    return sum;
                }
            }
            public virtual bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null) return false;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if (_penaltytimes[i] == 10)
                        {
                            return true;
                        }
                    }
                    return false;
                }
            }

            // конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _penaltytimes = new int[0];
            }

            // остальные методы
            public virtual void PlayMatch(int time)
            {
                if (_penaltytimes == null) return;
                Array.Resize(ref _penaltytimes, _penaltytimes.Length + 1);
                _penaltytimes[_penaltytimes.Length - 1] = time;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].Total > array[j + 1].Total)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }
            //public static Participant[] Iscluchen(Participant[] array)
            //{
            //    if (array == null) return null;
            //    Participant[] arr = new Participant[0];
            //    for (int i = 0; i < array.Length; i++)
            //    {
            //        if (array[i].IsExpelled == true)
            //        {
            //            Array.Resize(ref arr, arr.Length + 1);
            //            arr[arr.Length - 1] = array[i];
            //        }
            //    }
            //    return arr;

            //}

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} {Total} {IsExpelled}");
            }
        }
        //класс-наследник
        public class BasketballPlayer : Participant
        {
            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
                _penaltytimes = new int[0];
            }
            public override bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null) return false;
                    int count = 0;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if (_penaltytimes[i] == 5) count++;
                    }
                    if (Total > 2 * _penaltytimes.Length || count > 0.1 * _penaltytimes.Length)
                    {
                        return true;
                    }
                    return false;
                }
            }
            public override void PlayMatch(int foul)
            {
                if (_penaltytimes == null) return;
                if (foul < 0 || foul>5) return;
                base.PlayMatch(foul);
                
            }
        }
        //класс-наследник
        public class HockeyPlayer : Participant
        {
            private static int _alltime;
            private static int _count;
            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                _penaltytimes = new int[0];
                _count++;
            }
            public override void PlayMatch(int time)
            {
                if (_penaltytimes == null) return;
                base.PlayMatch(time);
                _alltime += time;
            }
            public override bool IsExpelled
            {
                get
                {
                    if (_penaltytimes == null) return false;
                    for (int i = 0; i < _penaltytimes.Length; i++)
                    {
                        if (_penaltytimes[i] == 10)
                        {
                            return true;
                        }
                    }
                    if (Total > 0.1 * _alltime / _count) return true;
                    return false;
                }
            }
        }
    }
}
