
using System.Collections.Generic;
using System.Linq;
using Aimtec;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal partial class Caitlyn
    {
        #region Fields

        /// <summary>
        ///     Initializes the enemy's trap data.
        /// </summary>
        public Dictionary<int, double> EnemyTrapData = new Dictionary<int, double>();

        /// <summary>
        ///     Initializes the trap time check for each enemy.
        /// </summary>
        public void InitializeTrapTimeCheck()
        {
            foreach (var hero in GameObjects.EnemyHeroes)
            {
                EnemyTrapData.Add(hero.NetworkId, 0d);
            }
        }

        /// <summary>
        ///     Gets an enemy's last Trap time.
        /// </summary>
        public double GetLastEnemyTrapTime(int networkId)
        {
            if (EnemyTrapData.ContainsKey(networkId))
            {
                return EnemyTrapData.FirstOrDefault(k => k.Key == networkId).Value;
            }

            return 0;
        }

        /// <summary>
        ///     Returns true if an enemy can be trapped, else, false.
        /// </summary>
        public bool CanTrap(Obj_AI_Hero hero)
        {
            if (!hero.IsEnemy)
            {
                return false;
            }

            if (hero.IsImmobile(SpellClass.W.Delay))
            {
                return true;
            }

            return Game.TickCount - GetLastEnemyTrapTime(hero.NetworkId) >= 4000 - SpellClass.W.Delay * 1000;
        }

        /// <summary>
        ///     Updates an enemy's last Trap time.
        /// </summary>
        public void UpdateEnemyTrapTime(int networkId)
        {
            if (EnemyTrapData.ContainsKey(networkId))
            {
                EnemyTrapData.Remove(networkId);
            }

            EnemyTrapData.Add(networkId, Game.TickCount);
        }

        #endregion
    }
}