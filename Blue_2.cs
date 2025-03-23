using System;



namespace Lab_7
{
    public class Blue_2
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int[,] _marks;
            private int _globalI;
            public string Name => _name;
            public string Surname => _surname;
            public int[,] Marks
            {
                get
                {
                    if (_marks == null) return default(int[,]);
                    else
                    {
                        int[,] marks = new int[_marks.GetLength(0), _marks.GetLength(1)];
                        for (int i = 0; i < _marks.GetLength(0); i++)
                            for (int j = 0; j < _marks.GetLength(1); j++)
                                marks[i, j] = _marks[i, j];
                        return marks;
                    }
                }
            }

            public int TotalScore
            {
                get
                {
                    if (_marks == null) return 0;
                    else
                    {
                        int sum = 0;
                        for (int i = 0; i < _marks.GetLength(0); i++)
                            for (int j = 0; j < _marks.GetLength(1); j++)
                                sum += _marks[i, j];
                        return sum;
                    }
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _marks = new int[2, 5];
                _globalI = 0;
            }

            public void Jump(int[] result)
            {
                if (result == null || _globalI > 1 || _marks == null) return;

                if (_globalI == 0)
                {

                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        _marks[0, j] = result[j];
                    }
                    _globalI++;
                }
                else if (_globalI == 1)
                {
                    for (int j = 0; j < _marks.GetLength(1); j++)
                    {
                        _marks[1, j] = result[j];
                    }
                    _globalI++;
                }
            }





            public static void Sort(Participant[] array)
            {

                if (array == null) return;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j + 1].TotalScore > array[j].TotalScore)
                        {
                            (array[j + 1], array[j]) = (array[j], array[j + 1]);
                        }
                    }
                }
            }

            public void Print()
            {
                Console.Write("{0,-20} {1,-20}", Name, Surname);
            }
        }

        public abstract class WaterJump
        {
            private int _index;
            public string Name { get; private set; }
            public int Bank { get; private set; }
            public Participant[] Participants { get; private set; }
            public abstract double[] Prize { get; }

            public WaterJump(string name, int bank)
            {
                Name = name;
                Bank = bank;
                Participants = new Participant[100];
                _index = 0;
            }

            public void Add(Participant participant)
            {
                if (Participants == null || _index>=Participants.Length) return;
                
                    Participants[_index++] = participant;
                
            }
            public void Add(Participant[] participants)
            {
                if (Participants == null || participants == null) return;

                for (int i = 0; i < participants.Length; i++)
                {
                    if (_index >= Participants.Length) break;
                    Add(participants[i]);
                }

            }
            public int GetIndex()
            {
                return _index;
            }
        }

        public class WaterJump3m : WaterJump
        {
            public WaterJump3m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    int count = GetIndex();
                    if (count < 3) return null;
                    double[] prizes = new double[3];
                    if (Participants.Length < 3) return null;
                    prizes[0] = 0.5 * Bank;
                    prizes[1] = 0.3 * Bank;
                    prizes[2] = 0.2 * Bank;
                    return prizes;
                }
            }

        }
        public class WaterJump5m: WaterJump
        {
            public WaterJump5m(string name, int bank) : base(name, bank) { }

            public override double[] Prize
            {
                get
                {
                    int count = GetIndex();
                    if (count < 3) return null;
                    int n = Participants.Length / 2;
                    double[] prizes = new double[n];
                    if (Participants.Length < 3 || n < 3 || n > 10) return null;

                    else
                    {
                        n = 20 / n;
                        
                        prizes[0] = 0.4 * Bank + n / 100 * Bank;
                        prizes[1] = 0.25 * Bank + n / 100 * Bank;
                        prizes[2] = 0.15 * Bank + n / 100 * Bank;
                        for (int i = 3; i < n; i++)
                        {
                            prizes[i] = n / 100 * Bank;
                        }
                    }
                    return prizes;
                }
            }
        }
    }
}

