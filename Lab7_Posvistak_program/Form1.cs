using Lab7_Posvistak_program;
using MonsterClass;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab7_Posvistak_program
{
    public partial class MainProgram : Form
    {
        MonsterManager manager = new MonsterManager();

        public MainProgram()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            manager.AddMonster(100, 20, "Kort1", MonsterType.Demon, WeaponType.Bow, true);
            manager.AddMonster(120, 30, "Kort2", MonsterType.None, WeaponType.Bow, true);
            manager.AddMonster(150, 50, "Kort3", MonsterType.Dragon, WeaponType.Bow, true);

            AddInList(ListBox1, manager.monsters[0]);
            AddInList(ListBox1, manager.monsters[1]);
            AddInList(ListBox1, manager.monsters[2]);
        }

        //* Отримання значення з textBox1 до MAX_NUMBER_OF_ARRAY та перевірка   
        //* цього значення на правельність. Увімкнення всьго інтерфейсу         
        public void Unlock_interface_Click(object sender, EventArgs e)
        {
            int count = -1; // запис числа в тимчасову комірку
            bool isNumber = true; // для перевірки на число вводу

            isNumber = checkForNumber(textBox1.Text);

            if (isNumber)
                count = Convert.ToInt32(textBox1.Text);
            else
                textBox1.Text = "Введіть цифру.";

            if (isNumber && count > 3 && count < 21)
            {
                manager.MAX_NUMBER_OF_ARRAY = count;

                label1.Visible = false;
                label1.Enabled = false;

                textBox1.Visible = false;
                textBox1.Enabled = false;

                button1.Enabled = false;
                button1.Visible = false;

                //***** Увімкнути всі інші можливості *****
                comboBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                textBox8.Enabled = true;
                comboBox2.Enabled = true;
                comboBox3.Enabled = true;
                comboBox4.Enabled = true;
                comboBox5.Enabled = true;

                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
                ListBox1.Enabled = true;
                ListBox2.Enabled = true;
                //*****************************************
            }
            else if (isNumber)
                textBox1.Text = "Введіть вірну цифру.";
        }

        //* Додавання в змінну "monsters" новий елемент та перевірка на         
        //* правельність внесення даних. Додавання цієї інформації в ListBox    
        public void Add_in_list_Click(object sender, EventArgs e)
        {
            float HP = 0;
            float Damage = 0;
            string Name = textBox5.Text;
            MonsterType monsterType = MonsterType.None;
            WeaponType weaponType = WeaponType.None;
            bool hostile = true;

            if (comboBox1.Text != "")
                monsterType = (MonsterType)Enum.Parse(typeof(MonsterType), comboBox1.Text);

            if (comboBox2.Text != "")
                weaponType = (WeaponType)Enum.Parse(typeof(WeaponType), comboBox2.Text);

            if (comboBox3.Text != "")
                hostile = Convert.ToBoolean(comboBox3.Text);


            Monster monster;

            bool isNumber = checkForNumber(textBox3.Text);
            bool isNumber2 = checkForNumber(textBox4.Text);

            if (isNumber && isNumber2)
            {
                HP = float.Parse(textBox3.Text);
                Damage = float.Parse(textBox4.Text);
            }

            if (isNumber && isNumber2 && manager.MAX_NUMBER_OF_ARRAY > manager.monsters.Count && checkTextForNull() == 1)
            {
                Monster.TryParse($"{HP}, {Damage}, {Name}, {monsterType}, {weaponType}, {hostile}", out monster);

                if (monster != null && Monster.checkForRules(HP, Damage, Name))
                {
                    AddNewElement(monster);
                }
            }
            else if (checkTextForNull() == 2)
            {
                monster = new Monster();
                AddNewElement(monster);
            }
            else if (isNumber && isNumber2 && checkTextForNull() == 3)
            {
                Monster.TryParse($"{HP}, {Damage}, {Name}", out monster);
                if (monster != null && Monster.checkForRules(HP, Damage, Name))
                {
                    AddNewElement(monster);
                }
            }

            if (manager.MAX_NUMBER_OF_ARRAY == manager.monsters.Count)
            { label8.Text = $"Дойдено до максимального ліміту ({manager.monsters.Count})"; }
        }

        // перевіряє які поля заповнені та передає значення який конструктор має бути використаний
        public int checkTextForNull()
        {
            int Ret = 0;
            if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != ""
            && comboBox1.Text != "" && comboBox2.Text != "" && comboBox3.Text != "" && manager.MAX_NUMBER_OF_ARRAY > manager.monsters.Count)
                Ret = 1;
            else
            if (textBox3.Text == "" && textBox4.Text == "" && textBox5.Text == ""
                && comboBox1.Text == "" && comboBox2.Text == "" && comboBox3.Text == "" && manager.MAX_NUMBER_OF_ARRAY > manager.monsters.Count)
                Ret = 2;
            else
            if (comboBox1.Text == "" && comboBox2.Text == "" && comboBox3.Text == "" && manager.MAX_NUMBER_OF_ARRAY > manager.monsters.Count)
                Ret = 3;

            return Ret;
        }

        // Додає новий елемент в List monsters та виводить його на екран
        public void AddNewElement(Monster monster)
        {
            manager.AddMonster(monster);
            AddInList(ListBox1, monster);
            label8.Text = $"Додано новый елемент, тепер їх ({manager.monsters.Count})";
            label22.Text = $"Кількість елементів у List = {Monster.count}";
        }

        //* Додаткова функція для перевірки користувач вписав число чи ні       
        public bool checkForNumber(string textBox)
        {
            float f = 0;
            int k = 0;
            bool resultB = false;
            if (textBox.Length == 0)
                resultB = false;

            foreach (var item in textBox)
            {
                if (item < 48 && item > 57 && item != '-' && item != ',')
                { resultB = false; break; }
            }

            if (float.TryParse(textBox, out f))
                resultB = true;

            if (int.TryParse(textBox, out k))
                resultB = true;

            return resultB;
        }

        //* Знайти за індексом або за типом монстра інформації про нього в      
        //* "monsters" та вивисти її на інший ListBox.                          
        public void findByIndexOrType(object sender, EventArgs e)
        {
            ListBox2.Items.Clear();
            label15.Text = "";
            string msg = "";
            int index = 0;
            MonsterType FindbyMonsterType = MonsterType.None;

            if (checkForNumber(textBox2.Text))
            {
                comboBox4.SelectedIndex = -1;
                index = Convert.ToInt32(textBox2.Text);
                Monster monster = manager.FindMonsterByIndex(index, out msg);
                if (monster != null)
                {
                    AddInList(ListBox2, monster);
                }
                else
                    label15.Text = "Не вірний індекс!";
            }
            else
                label15.Text = "Введіть вірне значення.";

            if (comboBox4.Text.Length != 0)
            {
                FindbyMonsterType = (MonsterType)Enum.Parse(typeof(MonsterType), comboBox4.Text);
                msg = "";
                List<Monster> list = manager.FindMonsterByType(FindbyMonsterType, out msg);
                AddInList(ListBox2, list);
                label15.Text = msg;
            }
        }


        //* Додаткова функція для оновлення ListBox якщо в "monsters"
        //* були внесенні зміни                                      
        public void refreshList(ListBox listBox)
        {
            listBox.Items.Clear();
            foreach (var item in manager.monsters)
            {
                AddInList(listBox, item);
            }
        }


        //* Додаткова функція для додавання в "monsters" нових елементів
        public void AddInList(ListBox listBox, Monster item)
        {
            listBox.Items.Add($"ID:{item.monsterID}" +
                               $" - Type:{item.monsterType}" +
                               $" - HP:{item.health}" +
                               $" - Damage:{item.damage}" +
                               $" - Name:{item.name}" +
                               $" - Weapon:{item.weapon}" +
                               $" - Hostile:{item.GetHostile()}");
        }
        public void AddInList(ListBox listBox, List<Monster> Monsters)
        {
            listBox.Items.Clear();
            foreach (var item in Monsters)
            {
                listBox.Items.Add($"ID:{item.monsterID}" +
                               $" - Type:{item.monsterType}" +
                               $" - HP:{item.health}" +
                               $" - Damage:{item.damage}" +
                               $" - Name:{item.name}" +
                               $" - Weapon:{item.weapon}" +
                               $" - Hostile:{item.GetHostile()}");
            }
        }


        //* Видалити за індексом або за типом монстра інформації про нього в      
        //* "monsters" та вивисти її на інший ListBox.                            
        public void delByIndexOrType(object sender, EventArgs e)
        {
            label15.Text = "";
            string msg = "";
            int index = 0;
            MonsterType FindbyMonsterType = MonsterType.None;

            if (checkForNumber(textBox6.Text))
            {
                comboBox5.SelectedIndex = -1;
                index = int.Parse(textBox6.Text);
                manager.DeleteMonsterByIndex(index, out msg);
                label15.Text = msg;
            }
            else
                label15.Text = "Введіть вірне значення.";

            if (comboBox5.Text.Length != 0)
            {
                FindbyMonsterType = (MonsterType)Enum.Parse(typeof(MonsterType), comboBox5.Text);
                msg = "";
                manager.DeleteMonsterByType(FindbyMonsterType, out msg);
                AddInList(ListBox2, manager.monsters);
                label15.Text = msg;
            }
            refreshList(ListBox1);
        }


        //* Додаткова функція для пошуку та додавання або віднімання здоров'я вибраному
        //* монстру за індексом.
        public void UseClassMethodForButtons(bool Heal = true)
        {
            label18.Text = string.Empty;
            string msg = string.Empty;
            int ID = 0;

            if (checkForNumber(textBox7.Text))
            {
                ID = Convert.ToInt32(textBox7.Text);

                if (Heal)
                    manager.HealMonster(ID, out msg);
                else
                    manager.ReduceHealMonster(ID, out msg);

                refreshList(ListBox1);
                label18.Text = msg;
            }
            else
                label18.Text = "Введіть існуючий ID.";
        }


        //* використовує UseClassmethodForButtons для додавання здоров'я вибраному монстру за індексом.
        public void Heal(object sender, EventArgs e)
        {
            UseClassMethodForButtons();
        }


        //* використовує UseClassmethodForButtons для віднімання здоров'я вибраному монстру за індексом.
        public void reduceHeal(object sender, EventArgs e)
        {
            UseClassMethodForButtons(Heal: false);
        }

        public void clear_button_Click(object sender, EventArgs e)
        {
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
        }

        public void Add_by_string_Click(object sender, EventArgs e)
        {
            Monster monster;
            Monster.TryParse(textBox8.Text, out monster);

            if (monster != null)
            {
                AddNewElement(monster);
            }
        }

        private void saveFile(object sender, EventArgs e)
        {
            string path;
            if (radioButton1.Checked)
                saveFileDialog1.Filter = "CSV файлы (*.csv)|*.csv";
            else if (radioButton2.Checked)
                saveFileDialog1.Filter = "JSON файлы (*.json)|*.json";

            if (radioButton1.Checked && saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog1.FileName;
                MessageBox.Show(Monster.SaveToFileCSV(manager.monsters, path));
            }
            else if (radioButton2.Checked && saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = saveFileDialog1.FileName;
                MessageBox.Show(Monster.SaveToFileJson(manager.monsters, path));
            }
        }

        private void readFile(object sender, EventArgs e)
        {
            string path, exeption;
            if (radioButton1.Checked)
                openFileDialog1.Filter = "CSV файлы (*.csv)|*.csv";
            else if (radioButton2.Checked)
                openFileDialog1.Filter = "JSON файлы (*.json)|*.json";

            if (radioButton1.Checked && openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                foreach (var item in Monster.ReadFromFileCSV(path, out exeption))
                {
                    manager.monsters.Add(item);
                }
                refreshList(ListBox1);
                MessageBox.Show(exeption);
            }
            else if (radioButton2.Checked && openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                foreach (var item in Monster.ReadFromFileJson(path, out exeption))
                {
                    manager.monsters.Add(item);
                }
                refreshList(ListBox1);
                MessageBox.Show(exeption);
            }
        }

        private void clearList(object sender, EventArgs e)
        {
            manager.monsters.Clear();
            refreshList(ListBox1);
        }
    }
}