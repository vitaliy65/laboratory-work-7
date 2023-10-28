using MonsterClass;
using Lab7_Posvistak_program;

namespace TestProject1
{
    [TestClass]
    public class MonsterTest
    {
        [TestMethod]
        public void checkForRulesFloatTest_values_good_for_rules()
        {
            // Arrange
            float HP = 100;
            float Damage = 20;
            string name = "qwer";
            bool isCorrectFloat = false;

            // Act
            isCorrectFloat = Monster.checkForRules(HP, Damage, name);

            // Assert
            Assert.IsTrue(isCorrectFloat);
        }

        [TestMethod]
        public void checkForRulesIntTest_values_good_for_rules()
        {
            // Arrange
            int HP = 100;
            int Damage = 20;
            string name = "qwer";
            bool isCorrectInt = false;

            // Act
            isCorrectInt = Monster.checkForRules(HP, Damage, name);

            // Assert
            Assert.IsTrue(isCorrectInt);
        }

        [TestMethod]
        public void checkForRulesFloatTest_values_bad_for_rules()
        {
            // Arrange
            float HP = 300;
            float Damage = 60;
            string name = "qwer";
            bool isCorrectFloat = false;

            // Act
            isCorrectFloat = Monster.checkForRules(HP, Damage, name);

            // Assert
            Assert.IsFalse(isCorrectFloat);
        }

        [TestMethod]
        public void checkForRulesIntTest_values_bad_for_rules()
        {
            // Arrange
            int HP = 300;
            int Damage = 60;
            string name = "qwer";
            bool isCorrectFloat = false;

            // Act
            isCorrectFloat = Monster.checkForRules(HP, Damage, name);

            // Assert
            Assert.IsFalse(isCorrectFloat);
        }

        [TestMethod]
        public void ParseTest_check_for_correct_parse()
        {
            // Arrange
            float value1 = 100;
            float value2 = 20;
            string s = "qwer";
            Monster actual = new Monster();
            Monster expexted = new Monster(value1, value2, s);

            // Act
            actual = Monster.Parse($"{value1},{value2},{s}");

            // Assert
            Assert.AreEqual(expexted.health, actual.health);
            Assert.AreEqual(expexted.damage, actual.damage);
            Assert.AreEqual(expexted.name, actual.name);
            Assert.AreEqual(expexted.monsterType, actual.monsterType);
            Assert.AreEqual(expexted.weapon, actual.weapon);
            Assert.AreEqual(expexted.hostile, actual.hostile);
        }

        [TestMethod]
        public void toStringTest_check_for_correct_change()
        {
            // Arrange
            Monster monster = new Monster(200, 50, "QWER", MonsterType.Demon, WeaponType.None, true);

            // Act
            string result = monster.ToString();
            string expected = "200, 50, QWER, Demon, None, True";

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void ParseTest_check_Format_throw_FormatException()
        {
            // Arrange
            Monster monster = new Monster();

            // Act + Assert
            Assert.ThrowsException<FormatException>(() => Monster.Parse("Demon,20,100"));
        }

        [TestMethod]
        public void ParseTest_check_Argument_throw_ArgumentException()
        {
            // Arrange
            Monster monster = new Monster();

            // Act + Assert
            Assert.ThrowsException<ArgumentException>(() => Monster.Parse("100,20,Demon,ASD"));
        }

        [TestMethod]
        public void ParseTest_check_Exception_throw_Exception()
        {
            // Arrange
            Monster monster = new Monster();

            // Act + Assert
            Assert.ThrowsException<Exception>(() => Monster.Parse("100,20"));
        }

        [TestMethod]
        public void TryParseTest_values_good_for_parse()
        {
            // Arrange
            Monster monster = new Monster();
            string s = "100,20,ASDF";
            bool isCorrect = false;

            // Act
            isCorrect = Monster.TryParse(s, out monster);

            // Assert
            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void TryParseTest_values_bad_for_parse()
        {
            // Arrange
            Monster monster = new Monster();
            string s = "ASDF,20,100";
            bool isCorrect = false;

            // Act
            isCorrect = Monster.TryParse(s, out monster);

            // Assert
            Assert.IsFalse(isCorrect);
        }



        // ---------- перевірка на збереження файлу ----------
        [TestMethod]
        public void TrySaveToFileCSV_argument_for_save_properly()
        {
            // Arrange
            List<Monster> monsters = new List<Monster>();
            string path = "temp.csv";

            // Act
            string res = Monster.SaveToFileCSV(monsters, path);

            // Assert
            Assert.AreSame(res, "Файл було успішно створено!");
        }
        [TestMethod]
        public void TrySaveToFileJson_argument_for_save_properly()
        {
            // Arrange
            List<Monster> monsters = new List<Monster>();
            string path = "temp.json";

            // Act
            string res = Monster.SaveToFileJson(monsters, path);

            // Assert
            Assert.AreSame(res, "Файл було успішно створено!");
        }
        [TestMethod]
        public void TrySaveToFileCSV_argument_for_save_not_correct()
        {
            // Arrange
            List<Monster> monsters = new List<Monster>();
            string path = "ASafsd\\temp.csv";

            // Act
            string res = Monster.SaveToFileCSV(monsters, path);

            // Assert
            Assert.AreNotSame(res, "Файл було успішно створено!");
        }
        [TestMethod]
        public void TrySaveToFileJson_argument_for_save_not_correct()
        {
            // Arrange
            List<Monster> monsters = new List<Monster>();
            string path = "ASafsd\\temp.json";

            // Act
            string res = Monster.SaveToFileJson(monsters, path);

            // Assert
            Assert.AreNotSame(res, "Файл було успішно створено!");
        }
        // ---------------------------------------------------



        // ---------- перевірка на зчитування файлу ----------
        [TestMethod]
        public void TryReadFromFileCSV_argument_for_read_properly()
        {
            // Arrange
            string path = "temp.csv";
            string exeption = "";

            // Act
            Monster.ReadFromFileCSV(path, out exeption);

            // Assert
            Assert.AreSame(exeption, "Успішно додано нові дані до List!");
        }
        [TestMethod]
        public void TryReadFromFileJson_argument_for_read_properly()
        {
            // Arrange
            string path = "temp.json";
            string exeption = "";

            // Act
            Monster.ReadFromFileJson(path, out exeption);

            // Assert
            Assert.AreSame(exeption, "Успішно додано нові дані до List!");
        }
        [TestMethod]
        public void TryReadFromFileCSV_argument_for_read_not_correct()
        {
            // Arrange
            string path = "asga\\temp.json";
            string exeption = "";

            // Act
            Monster.ReadFromFileCSV(path, out exeption);

            // Assert
            Assert.AreNotSame(exeption, "Успішно додано нові дані до List!");
        }
        [TestMethod]
        public void TryReadFromFileJson_argument_for_read_not_correct()
        {
            // Arrange
            string path = "asga\\temp.json";
            string exeption = "";

            // Act
            Monster.ReadFromFileJson(path, out exeption);

            // Assert
            Assert.AreNotSame(exeption, "Успішно додано нові дані до List!");
        }
        // -------------------------------------------------- 
    }

    [TestClass]
    public class MonsterManagerTests
    {
        [TestMethod]
        public void TestAddMonster_valid_values()
        {
            // Arrange
            var manager = new MonsterManager();

            // Act
            manager.AddMonster(100, 10, "TestMonster", MonsterType.Goblin, WeaponType.Sword, true);

            // Assert
            Assert.AreEqual(1, manager.monsters.Count);
        }

        [TestMethod]
        public void TestAddMonster_valid_parameters()
        {
            // Arrange
            var manager = new MonsterManager();
            var monster = new Monster(100, 10, "TestMonster", MonsterType.Goblin, WeaponType.Sword, true);

            // Act
            manager.AddMonster(monster);

            // Assert
            Assert.AreEqual(1, manager.monsters.Count);
            Assert.AreEqual(monster, manager.monsters[0]);
        }

        [TestMethod]
        public void TestFindMonsterByIndex_valid_parameters()
        {
            // Arrange
            var manager = new MonsterManager();
            var monster = new Monster(100, 10, "TestMonster", MonsterType.Goblin, WeaponType.Sword, true);
            manager.AddMonster(monster);

            // Act
            string exceptionMsg;
            var foundMonster = manager.FindMonsterByIndex(0, out exceptionMsg);

            // Assert
            Assert.AreEqual(monster, foundMonster);
            Assert.AreEqual(string.Empty, exceptionMsg);
        }

        [TestMethod]
        public void TestFindMonsterByType_valid_parameters()
        {
            // Arrange
            var manager = new MonsterManager();
            var monster1 = new Monster(100, 10, "Monster1", MonsterType.Goblin, WeaponType.Sword, true);
            var monster2 = new Monster(150, 20, "Monster2", MonsterType.Dragon, WeaponType.Stick, true);
            manager.AddMonster(monster1);
            manager.AddMonster(monster2);

            // Act
            string exceptionMsg;
            var foundMonsters = manager.FindMonsterByType(MonsterType.Goblin, out exceptionMsg);

            // Assert
            CollectionAssert.Contains(foundMonsters, monster1);
            CollectionAssert.DoesNotContain(foundMonsters, monster2);
            Assert.AreEqual(string.Empty, exceptionMsg);
        }

        [TestMethod]
        public void TestDeleteMonsterByIndex_valid_parameters()
        {
            // Arrange
            var manager = new MonsterManager();
            var monster = new Monster(100, 10, "TestMonster", MonsterType.Goblin, WeaponType.Sword, true);
            manager.AddMonster(monster);

            // Act
            string exceptionMsg;
            manager.DeleteMonsterByIndex(0, out exceptionMsg);

            // Assert
            Assert.AreEqual(0, manager.monsters.Count);
            Assert.AreEqual(string.Empty, exceptionMsg);
        }

        [TestMethod]
        public void TestDeleteMonsterByType_valid_parameters()
        {
            // Arrange
            var manager = new MonsterManager();
            var monster = new Monster(100, 10, "TestMonster", MonsterType.Goblin, WeaponType.Sword, true);
            manager.AddMonster(monster);

            // Act
            string exceptionMsg;
            manager.DeleteMonsterByType(MonsterType.Goblin, out exceptionMsg);

            // Assert
            Assert.AreEqual(0, manager.monsters.Count);
            Assert.AreEqual(string.Empty, exceptionMsg);
        }

        [TestMethod]
        public void TestHealMonster_valid_parameters()
        {
            // Arrange
            var manager = new MonsterManager();
            var monster = new Monster(100, 10, "TestMonster", MonsterType.Goblin, WeaponType.Sword, true);
            manager.AddMonster(monster);

            // Act
            string msg;
            manager.HealMonster(0, out msg);

            // Assert
            Assert.AreEqual("", msg);
        }

        [TestMethod]
        public void TestReduceHealMonster_valid_parameters()
        {
            // Arrange
            var manager = new MonsterManager();
            var monster = new Monster(100, 10, "TestMonster", MonsterType.Goblin, WeaponType.Sword, true);
            manager.AddMonster(monster);

            // Act
            string msg;
            manager.ReduceHealMonster(0, out msg);

            // Assert
            Assert.AreEqual("", msg);
        }

        // -------------------------------------------------

        [TestMethod]
        public void TestAddMonsterWithNull()
        {
            // Arrange
            var manager = new MonsterManager();
            Monster monster = null;

            // Act
            manager.AddMonster(monster);

            // Assert
            Assert.AreEqual(0, manager.monsters.Count);
        }

        [TestMethod]
        public void TestFindMonsterByInvalidIndex()
        {
            // Arrange
            var manager = new MonsterManager();
            var monster = new Monster(100, 10, "TestMonster", MonsterType.Goblin, WeaponType.Sword, true);
            manager.AddMonster(monster);

            // Act
            string exceptionMsg;
            var foundMonster = manager.FindMonsterByIndex(3, out exceptionMsg);

            // Assert
            Assert.IsNull(foundMonster);
        }

        [TestMethod]
        public void TestFindMonsterByType_Invalid_parameters()
        {
            // Arrange
            var manager = new MonsterManager();
            var monster1 = new Monster(100, 10, "Monster1", MonsterType.Goblin, WeaponType.Sword, true);
            var monster2 = new Monster(150, 20, "Monster2", MonsterType.Dragon, WeaponType.Stick, true);
            manager.AddMonster(monster1);
            manager.AddMonster(monster2);

            // Act
            string exceptionMsg;
            var foundMonsters = manager.FindMonsterByType(MonsterType.None, out exceptionMsg);

            // Assert
            Assert.AreNotEqual(foundMonsters.Count, 2);
        }

        [TestMethod]
        public void TestDeleteMonsterWithInvalidIndex()
        {
            // Arrange
            var manager = new MonsterManager();
            var monster = new Monster(100, 10, "TestMonster", MonsterType.Goblin, WeaponType.Sword, true);
            manager.AddMonster(monster);

            // Act
            string exceptionMsg;
            manager.DeleteMonsterByIndex(1, out exceptionMsg);

            // Assert
            Assert.AreEqual(1, manager.monsters.Count);
        }

        [TestMethod]
        public void TestDeleteMonsterByType_Invalid_parameters()
        {
            // Arrange
            var manager = new MonsterManager();
            var monster = new Monster(100, 10, "TestMonster", MonsterType.Goblin, WeaponType.Sword, true);
            manager.AddMonster(monster);

            // Act
            string exceptionMsg;
            manager.DeleteMonsterByType(MonsterType.Goblin, out exceptionMsg);

            // Assert
            Assert.AreNotEqual(manager.monsters.Count, 2);
        }

        [TestMethod]
        public void TestHealMonsterWithInvalidID()
        {
            // Arrange
            var manager = new MonsterManager();
            var monster = new Monster(100, 10, "TestMonster", MonsterType.Goblin, WeaponType.Sword, true);
            manager.AddMonster(monster);

            // Act
            string msg;
            manager.HealMonster(25, out msg);

            // Assert
            Assert.AreEqual("Такого ID не існує", msg);
        }

        [TestMethod]
        public void TestReduceHealMonsterWithInvalidID()
        {
            // Arrange
            var manager = new MonsterManager();
            var monster = new Monster(100, 10, "TestMonster", MonsterType.Goblin, WeaponType.Sword, true);
            manager.AddMonster(monster);

            // Act
            string msg;
            manager.ReduceHealMonster(25, out msg);

            // Assert
            Assert.AreEqual("Такого ID не існує", msg);
        }
    }
}