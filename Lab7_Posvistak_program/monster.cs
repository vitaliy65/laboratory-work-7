using Lab7_Posvistak_program;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace MonsterClass
{
    public class Monster
    {
        static private int _forID = 0; // статік змінна для рахунку ID під час створення нового елемента та запис в MonsterID
        static private int _count = 0; // статік змінна для підрахунку
        private float _health; // здоров'я
        private float _Damage; // урон
        private string _name;  // ім'я
        private MonsterType _monsterType = MonsterType.None; // для типу монстра
        private WeaponType _weapon = WeaponType.None;  // тип зброї для монстра
        private bool _hostile;

        // public властивості
        static public int count
        {
            get { return _count; }
            set { _count = value; }
        }
        [JsonIgnore]
        public int getLastID
        {
            get { return _forID; }
        }
        [JsonIgnore]
        public int monsterID { get; private set; } = 0;
        public float health
        {
            get { return _health; }
            set { _health = value; }
        }
        public float damage
        {
            get { return _Damage; }
            set { _Damage = value; }
        }
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public MonsterType monsterType
        {
            get { return _monsterType; }
            set { _monsterType = value; }
        }
        public WeaponType weapon
        {
            get { return _weapon; }
            set { _weapon = value; }
        }
        public bool hostile
        {
            get { return _hostile; }
            set { _hostile = value; }
        }

        // обчислювальні властивості
        [JsonIgnore]
        public float addHealth //*** Зробити операцію додавання здоров'я монстру ***//
        {
            get
            {
                if (this.health < 200 && (this.health + 20) < 200)
                    return this.health += 20;
                else
                    return this.health = 200;
            }
        }
        [JsonIgnore]
        public float reduceHealth //*** Зробити операцію віднімання здоров'я монстру ***//
        {
            get
            {
                if (this.health > 0 && (this.health - 20) > 0)
                    return this.health -= 20;
                else
                    return this.health = 0;
            }
        }


        // Конструктор за замовчуванням
        public Monster()
        {
            monsterID = _forID; ++_forID;
            ++_count;
            this.health = 100;
            this.damage = 50;
            this.name = "default";
            this.monsterType = MonsterType.None;
            this.weapon = WeaponType.None;
            hostile = true;
        }
        // Конструктор який приймає 3 параметра та передає їх в 3-й конструктор
        public Monster(float health, float Damage, string name)
            : this(health, Damage, name, MonsterType.None, WeaponType.None, true) { }
        // Конструктор який приймає 6 параметрів та встановлює значення для полів класу
        public Monster(float health, float Damage, string name, MonsterType monsterType, WeaponType weaponType, bool hostile)
        {
            if (checkForRules(health, Damage, name))
            {
                monsterID = _forID; ++_forID;
                ++_count;
                this.health = health;
                this.damage = Damage;
                this.name = name;
                this.monsterType = monsterType;
                this.weapon = weaponType;
                this.hostile = hostile;
            }
        }


        //*** Перевірка для змінних HP, Damage, name на заданні обмеження ***//
        public static bool checkForRules(float HP, float Damage, string name)
        {
            if (HP > 0 && HP < 201 && Damage > 0 && Damage < 51 && name.Length > 3)
                return true;
            else
                return false;
        }
        // перезругка функції checkForRules з двома параметрами
        public static bool checkForRules(int HP, int Damage, string name)
        {
            if (HP > 0 && HP < 201 && Damage > 0 && Damage < 51 && name.Length > 3)
                return true;
            else
                return false;
        }

        public bool GetHostile() => hostile;

        public static void delOneElementFromCount() { --_count; }

        public static Monster Parse(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            string[] parts = s.Split(',');

            try
            {
                if (parts.Length != 3 || parts.Length != 6)
                {
                    if (parts.Length == 3)
                    {
                        return new Monster(
                        float.Parse(parts[0]),
                        float.Parse(parts[1]),
                        parts[2]
                        );
                    }
                    else
                    {
                        return new Monster(
                        float.Parse(parts[0]),
                        float.Parse(parts[1]),
                        parts[2],
                        (MonsterType)Enum.Parse(typeof(MonsterType), parts[3]),
                        (WeaponType)Enum.Parse(typeof(WeaponType), parts[4]),
                        bool.Parse(parts[5])
                        );
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message, ex);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static bool TryParse(string s, out Monster monster)
        {
            monster = null;
            bool valid = false;

            try
            {
                monster = Monster.Parse(s);
                valid = true;
            }
            catch (FormatException ex)
            {
                valid = false;
            }
            catch (ArgumentException ex)
            {
                valid = false;
            }
            catch (Exception ex)
            {
                valid = false;
            }

            return valid;
        }

        public override string ToString()
        {
            return $"{health}, {damage}, {name}, {monsterType}, {weapon}, {hostile}";
        }

        public static string SaveToFileCSV(List<Monster> monsters, string path)
        {
            List<string> lines = new List<string>();
            foreach (var item in monsters)
            {
                lines.Add(item.ToString());
            }

            try
            {
                File.WriteAllLines(path, lines);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Файл було успішно створено!";
        }

        public static List<Monster> ReadFromFileCSV(string path, out string msg)
        {
            msg = "Успішно додано нові дані до List!";

            List<Monster> accounts = new List<Monster>();
            try
            {
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(path).ToList();

                foreach (var item in lines)
                {
                    Console.WriteLine(item);
                }
                foreach (var item in lines)
                {
                    Monster? account;
                    bool result = Monster.TryParse(item, out account);
                    if (result) accounts.Add(account);
                }
            }
            catch (IOException ex)
            {
                msg = ex.Message;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return accounts;
        }

        public static string SaveToFileJson(List<Monster> monsters, string path)
        {
            try
            {
                string jsonstring = "";
                foreach (var item in monsters)
                {
                    jsonstring += JsonSerializer.Serialize<Monster>(item);
                    jsonstring += "\n";
                }
                File.WriteAllText(path, jsonstring);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Файл було успішно створено!";
        }

        public static List<Monster> ReadFromFileJson(string path, out string msg)
        {
            msg = "Успішно додано нові дані до List!";

            List<Monster> accounts = new List<Monster>();
            try
            {
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(path).ToList();
                foreach (var item in lines)
                {
                    Monster? account = JsonSerializer.Deserialize<Monster>(item);
                    if (account != null) accounts.Add(account);
                }
            }
            catch (IOException ex)
            {
                msg = ex.Message;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return accounts;
        }
    }
}