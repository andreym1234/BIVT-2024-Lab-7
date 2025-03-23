using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab_7.Blue_4;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            //поля
            private string _name;
            private int[] _scores;

            //cвойства
            public string Name { get { return _name; } }
            public int[] Scores
            {
                get
                {
                    if (_scores == null) return null;
                    int[] results = new int[_scores.Length];
                    int count = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        results[count++] = _scores[i];
                    }
                    return results;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (_scores == null) return 0;
                    int sum = 0;
                    for (int i = 0; i < _scores.Length; i++)
                    {
                        sum += _scores[i];
                    }
                    return sum;
                }
            }

            //конструктор
            public Team(string name)
            {
                _name = name;
                _scores = new int[0];
            }

            //остальные методы
            public void PlayMatch(int result)
            {
                if (_scores == null) return;
                Array.Resize(ref _scores, _scores.Length + 1);
                _scores[_scores.Length - 1] = result;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {TotalScore}");
            }
        }
        //класс-наследник
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name)
            {
            }
        }
        //класс-наследник
        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name)
            {
            }
        }
        //другой класс
        public class Group
        {
            //поля
            private string _name;
            private Team[] _manteams;
            private Team[] _womanteams;
            private int _mancount;
            private int _womancount;

            //свойства
            public string Name { get { return _name; } }
            public Team[] ManTeams { get { return _manteams; } }
            public Team[] WomanTeams { get { return _womanteams; } }

            //конструктор
            public Group(string name)
            {
                _name = name;
                _manteams = new Team[12];
                _womanteams = new Team[12];

                _mancount = 0;
                _womancount = 0;
            }

            //остальные методы
            public void Add(Team team)
            {
                if (team is ManTeam manteam && _mancount < _manteams.Length)
                {
                    _manteams[_mancount++] = manteam;
                }
                else if (team is WomanTeam womanteam && _womancount < _womanteams.Length)
                {
                    _womanteams[_womancount++] = womanteam;
                }
            }
            public void Add(Team[] teams)
            {
                foreach (Team team in teams)
                {
                    Add(team);
                }
            }
            public void SortSeparately(Team[] _teams)
            {
                if (_teams == null) return;
                for (int i = 0; i < _teams.Length; i++)
                {
                    for (int j = 0; j < _teams.Length - i - 1; j++)
                    {
                        if (_teams[j].TotalScore < _teams[j + 1].TotalScore)
                        {
                            (_teams[j], _teams[j + 1]) = (_teams[j + 1], _teams[j]);
                        }
                    }
                }
            }
            public void Sort()
            {
                SortSeparately(_manteams);
                SortSeparately(_womanteams);
            }
            public static Group Merge(Group group1, Group group2, int size)
            {
                Group merged = new Group("Финалисты");
                MergeSeparately(group1._manteams, group2._manteams, size / 2);
                MergeSeparately(group1._womanteams, group2._womanteams, size / 2);
                return merged;
            }
            public static void MergeSeparately(Team[] teams1, Team[] teams2, int size)
            {
                Team[] resultteam = new Team[teams1.Length+teams2.Length / 2];
                int i = 0, j = 0, k = 0;
                while (i < size && j < size)
                {
                    if (teams1[i].TotalScore >= teams2[j].TotalScore)
                    {
                        resultteam[k++] = teams1[i++];
                    } 
                    else
                    {
                        resultteam[k++] = teams2[j++];
                    }
                }
                while (i < size)
                {
                    resultteam[k++] = teams1[i++];
                }
                while (j < size)
                {
                    resultteam[k++] = teams2[j++];
                }
            }
            public void Print(Team[] _teams)
            {
                Console.WriteLine($"{Name} {_teams}");
            }
        }
    }
}
