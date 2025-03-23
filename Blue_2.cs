using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
        public abstract class WaterJump
        {
            private string _name;
            private int _bank;
            private Participant[] _participants;

            public string Name => _name;
            public int Bank => _bank;
            public Participant[] Participants => _participants;

            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                _name = name;
                _bank = bank;
                _participants = new Participant[0];

            }

            public void Add(Participant participant)
            {
                if (_participants == null) return;
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = participant;

            }
            public void Add(Participant[] participants)
            {
                if (_participants == null) return;
                for (int i = 0; i < participants.Length; i++)
                {
                    Add(participants[i]);
                }
            }
        }
        //класс-наследник
        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank)
            {
            }
            public override double[] Prize
            {
                get
                {
                    if (Participants == null) {
                        return null;
                    }
                    if (Participants.Length < 3) {
                        return null; 
                    }
                    Participant.Sort(Participants);
                    double[] prize = new double[3];
                    prize[0] = 0.5 * Bank;
                    prize[1] = 0.3 * Bank;
                    prize[2] = 0.2 * Bank;
                    return prize;
                }
            }
        }
        //класс-наследник
        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank)
            {
            }
            public override double[] Prize
            {
                get
                {
                    if (Participants == null)
                    {
                        return null;
                    }
                    if (Participants.Length < 3)
                    {
                        return null;
                    }
                    Participant.Sort(Participants);
                    double[] prize = new double[Participants.Length];
                    int count = 0;
                    for (int i = 0; i < prize.Length / 2; i++)
                    {
                        count++;
                    }
                    if (count > 10) count = 10;
                    double n = 20 / count;
                    for (int i = 0; i < count; i++)
                    {
                        prize[i] = n * Bank / 100;
                    }
                    prize[0] += 0.4 * Bank;
                    prize[1] += 0.25 * Bank;
                    prize[2] += 0.15 * Bank;
                    return prize;
                }
            }
        }
        // структура
        public struct Participant
        {
            // поля
            private string _name;
            private string _surname;
            private int[,] _marks;

            private int _count;

            // свойства
            public string Name { get { return _name; } }
            public string Surname { get { return _surname; } }
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return null;
                    int[,] results = new int[_marks.GetLength(0), _marks.GetLength(1)];
                    for (int i = 0; i < results.GetLength(0); i++)
                    {
                        for (int j = 0; j < results.GetLength(1); j++)
                        {
                            results[i, j] = _marks[i, j];
                        }
                    }
                    return results;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < _marks.GetLength(1); j++)
                        {
                            sum += _marks[i, j];
                        }
                    }
                    return sum;
                }
            }

            // конструктор
            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];

                _count = 0;
            }

            // остальные методы
            public void Jump(int[] result)
            {
                if (result == null || _marks == null || _count > 1) return;
                for (int j = 0; j < _marks.GetLength(1); j++)
                {
                    _marks[_count, j] = result[j];
                }
                _count++;

            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        }
                    }
                }
            }
            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} {TotalScore}");
            }
        }
    }
}
