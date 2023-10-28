using MonsterClass;
using System;
using System.Collections.Generic;

namespace Lab7_Posvistak_program
{
    public class MonsterManager
    {
        //* Змінна яка при на тисканні кнопки приймає значення з textbox для 
        //* максимального розміра List
        public int MAX_NUMBER_OF_ARRAY;
        public List<Monster> monsters = new List<Monster>();

        public void AddMonster(float HP, float damage, string name, MonsterType monsterType, WeaponType weaponType, bool hostile)
        {
            monsters.Add(new Monster(HP, damage, name, monsterType, weaponType, hostile));
        }

        public void AddMonster(Monster monster)
        {
            if(monster != null)
                monsters.Add(monster);
        }

        public Monster FindMonsterByIndex(int index, out string exeptionMsg)
        {
            exeptionMsg = string.Empty;
            try
            {
                if (index >= 0 && index < monsters.Count)
                {
                    return monsters[index];
                }
            }
            catch (Exception ex)
            {
                exeptionMsg = ex.Message;
            }

            return null;
        }

        public List<Monster> FindMonsterByType(MonsterType monsterType, out string exeptionMsg)
        {
            exeptionMsg = string.Empty;
            List<Monster> list = new List<Monster>();
            try
            {
                foreach (var item in monsters)
                {
                    if(item.monsterType == monsterType)
                    {
                        list.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                exeptionMsg = ex.Message;
            }

            return list;
        }

        public void DeleteMonsterByIndex(int index, out string exeptionMsg)
        {
            exeptionMsg = string.Empty;
            try
            {
                if (index >= 0 && index < monsters.Count)
                {
                    monsters.RemoveAt(index);
                }
            }
            catch (Exception ex)
            {
                exeptionMsg = ex.Message;
            }
        }

        public void DeleteMonsterByType(MonsterType monsterType, out string exeptionMsg)
        {
            exeptionMsg = string.Empty;
            List<Monster> list = new List<Monster>();
            foreach (var item in monsters)
            {
                list.Add(item);
            }

            try
            {
                foreach (var item in monsters)
                {
                    if (item.monsterType == monsterType)
                    {
                        list.Remove(item);
                    }
                }
            }
            catch (Exception ex)
            {
                exeptionMsg = ex.Message;
            }
            monsters = list;
        }

        public void HealMonster(int IDtoFind, out string msg)
        {
            msg = string.Empty;
            if(IDtoFind >= 0 && monsters[0].getLastID  > IDtoFind)
            {
                foreach(var item in monsters)
                {
                    if(item.monsterID == IDtoFind)
                    {
                        _ = item.addHealth;
                    }
                }
            }
            else if (monsters[0].getLastID - 1 < IDtoFind)
            {
                msg = "Такого ID не існує";
            }
            else
            {
                msg = "Введіть невід'ємний ID";
            }
        }

        public void ReduceHealMonster(int IDtoFind, out string msg)
        {
            msg = string.Empty;
            if (IDtoFind >= 0 && monsters[0].getLastID > IDtoFind)
            {
                foreach (var item in monsters)
                {
                    if (item.monsterID == IDtoFind)
                    {
                        _ = item.reduceHealth;
                    }
                }
            }
            else if (monsters[0].getLastID - 1 < IDtoFind)
            {
                msg = "Такого ID не існує";
            }
            else
            {
                msg = "Введіть невід'ємний ID";
            }
        }
    }
}
