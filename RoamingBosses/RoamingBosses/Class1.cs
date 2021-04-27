using GTA;
using GTA.Math;
using GTA.Native;
using System;
using NativeUI;
using System.Drawing;
using System.Collections.Generic;
namespace RoamingBosses
{
    public class RoamingBossProp : Script
    {
        public Prop Prop { get; set; }
        public int Timer { get; set; }
        public int ParticleFX1 { get; set; }
        public RoamingBossProp()
        {

        }
        public RoamingBossProp(Prop P, int T)
        {
            Prop = P;
            Timer = T;
        }
        public RoamingBossProp(Prop P,int PTFX1, int T)
        {
            Prop = P;
            Timer = T;
            ParticleFX1 = PTFX1;
        }
    }
    public class RoamingBoss:Script
    {
        public RoamingBoss()
        {

        }
        public Ped Boss { get; set; }
        public int Type { get; set; }
        public float SetHp { get; set; }
        public int Timer { get; set; }
        public List<int> Fire__ids = new List<int>();
        public List<int> PTFX__ids = new List<int>();
        public bool WaitingForStop { get; set; }
        public bool ReadyforMovement { get; set; }
        public int SpecialAttackTimer { get; set; }
        public int OutiftChangeTimer { get; set; }
        public Vector3 StartPoint { get; set; }
        public Vector3 Waypoint { get; set; }
        public Vector3 lastExplosiveCoord { get; set; }
        public List<Ped> DuplicatePeds = new List<Ped>();
        public bool WaitingForSpawn { get; set; }
        public bool OverrideTasks { get; set; }
        public Vector3 MovePoint { get; set; }
        public Ped CurrentPedSpawned { get; set; }
        public float SET_HP { get; set; }
        public float SET_ARMOUR { get; set; }
        public RoamingBoss(Ped boss,int type, Vector3 startpoint, int timer)
        {
            Type = type;
            StartPoint = startpoint;
            Timer = timer;
            Boss = boss;
            SET_HP = Boss.Armor;
            SET_ARMOUR = Boss.Health;
        }
        public RoamingBoss(Ped boss, int type, Vector3 startpoint, int timer,int timer2)
        {
            Type = type;
            StartPoint = startpoint;
            Timer = timer;
            OutiftChangeTimer = timer2;
            Boss = boss;
            SET_HP = Boss.Armor;
            SET_ARMOUR = Boss.Health;
        }
    }
    public class Class1 :Script
    {
        public Model RequestModel(string Name)
        {

            var model = new Model(Name);
            model.Request(10000);

            // Check the model is valid
            if (model.IsInCdImage && model.IsValid)
            {
                // Ensure the model is loaded before we try to create it in the world
                while (!model.IsLoaded) Script.Wait(150);
                return model;




            }

            // Mark the model as no longer needed to remove it from memory.

            model.MarkAsNoLongerNeeded();
            return model;
        }
        public Model RequestModel(VehicleHash Name)
        {

            var model = new Model(Name);
            model.Request(10000);

            // Check the model is valid
            if (model.IsInCdImage && model.IsValid)
            {
                // Ensure the model is loaded before we try to create it in the world
                while (!model.IsLoaded) Script.Wait(50);
                return model;




            }

            // Mark the model as no longer needed to remove it from memory.

            model.MarkAsNoLongerNeeded();
            return model;
        }
        public Model RequestModel(PedHash Name)
        {

            var model = new Model(Name);
            model.Request(10000);

            // Check the model is valid
            if (model.IsInCdImage && model.IsValid)
            {
                // Ensure the model is loaded before we try to create it in the world
                while (!model.IsLoaded) Script.Wait(50);
                return model;




            }

            // Mark the model as no longer needed to remove it from memory.

            model.MarkAsNoLongerNeeded();
            return model;
        }
        public Model RequestModel(Model model)
        {


            model.Request(10000);

            // Check the model is valid
            if (model.IsInCdImage && model.IsValid)
            {
                // Ensure the model is loaded before we try to create it in the world
                while (!model.IsLoaded) Script.Wait(50);
                return model;




            }

            // Mark the model as no longer needed to remove it from memory.

            model.MarkAsNoLongerNeeded();
            return model;
        }
        public List<RoamingBoss> Bosses = new List<RoamingBoss>();
        public bool firsttimeload;
        public List<int> Fire__ids = new List<int>();
        public List<int> PTFX__ids = new List<int>();
        public Class1()
        {
            LoadIniFile("scripts\\RoamingBosses.ini");
            Tick += OnTick;
            Aborted += OnShutdown;
            UI.Notify("~r~ Roaming Bossess ~w~ Loaded, created by ~g~HKH191~w~");
        }
        public bool ModOn;
        public ScriptSettings Config;
        public void LoadIniFile(string iniName)
        {
            try
            {
                Config = ScriptSettings.Load(iniName);


                //   Stage = Config.GetValue<int>("Setup", "Stage", Stage);
                ModOn = Config.GetValue<bool>("Config", "ModOn", ModOn);



                //RiotShieldOnShiftX=true
                //ShieldName=prop_ballistic_shield
                //"ShieldName can be prop_ballistic_shield, prop_ballistic_shield_lod1, prop_riot_shield"
            }
            catch (Exception e)
            {
                UI.Notify("~r~Error~w~: Config.ini Failed To Load.");
            }
        }
        public void SpawnPoliceJug(Vector3 S)
        {
            var Ped = World.CreatePed(RequestModel( PedHash.FreemodeMale01), S);
            Function.Call(Hash.SET_PED_PROP_INDEX, Ped, 0, 123, 0, 17);//hat
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 0, 0, 0, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 1, 57, 0, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 2, 0, 0, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 3, 46, 0, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 4, 84, 9, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 5, 0, 0, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 6, 10, 0, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 7, 0, 0, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 8, 97, 9, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 9, 0, 0, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 10, 0, 0, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 11, 186, 9, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 12, 0, 0, 1);
            Function.Call(Hash.SET_PED_USING_ACTION_MODE, Ped, true, -1, 0);
            Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, Ped, "ANIM_GROUP_MOVE_BALLISTIC", 5f);

            Function.Call(Hash.SET_PED_STRAFE_CLIPSET, Ped, "MOVE_STRAFE_BALLISTIC");
            Function.Call(Hash.SET_WEAPON_ANIMATION_OVERRIDE, Ped, 0x5534A626);
            int EnemyRelationShipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "HATES_PLAYER");



            Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, Ped, EnemyRelationShipGroup);
            Ped.RelationshipGroup = EnemyRelationShipGroup;

            Ped.Weapons.Give(WeaponHash.CombatMGMk2, 9999, true, true);
            Ped.CanSufferCriticalHits = false;
            Ped.CanWrithe = false;
            Ped.AddBlip();
            Ped.Health = 4000;
            Ped.Armor = 2000;
            Ped.CurrentBlip.Sprite = BlipSprite.Juggernaut;
            Ped.CurrentBlip.Color = BlipColor.Red;
            Ped.CurrentBlip.Name = "Roaming Boss";
            Ped.Accuracy = 10;
            Ped.CurrentBlip.Alpha = 0;

            Ped.CanRagdoll = false;
            Bosses.Add(new RoamingBoss(Ped,1,S,0));
        }
        public void SpawnClown(Vector3 S)
        {
            var Ped = World.CreatePed(RequestModel( PedHash.Clown01SMY), S);


            Ped.Weapons.Give(WeaponHash.Machete, 9999, true, true);
            Ped.CanSufferCriticalHits = false;
            Ped.CanWrithe = false;
            Ped.AddBlip();
            Ped.Health =3000;
         
            Ped.CurrentBlip.Sprite = BlipSprite.Juggernaut;
            Ped.CurrentBlip.Color = BlipColor.Red;
            Ped.CurrentBlip.Name = "Roaming Boss";
            Ped.CurrentBlip.Alpha = 0;
        Ped.Accuracy = 100;
            Ped.BlockPermanentEvents = true;
            Ped.RelationshipGroup = 5;
            Ped.AlwaysKeepTask = true;
         
            Ped.CanRagdoll = false;
            Bosses.Add(new RoamingBoss(Ped, 2, S, 0));
        }
        public void SpawnSlasher(Vector3 S)
        {
            var Ped = World.CreatePed((Model)0x696BE0A9, S);


            Ped.Weapons.Give(WeaponHash.Machete, 9999, true, true);
            Ped.CanSufferCriticalHits = false;
            Ped.CanWrithe = false;
            Ped.AddBlip();
            Ped.Health = 2900;
            Ped.IsFireProof = true;
            Ped.CurrentBlip.Sprite = BlipSprite.Juggernaut;
            Ped.CurrentBlip.Color = BlipColor.Red;
            Ped.CurrentBlip.Name = "Roaming Boss";
            Ped.CurrentBlip.Alpha = 0;
            Ped.Accuracy = 100;
            Ped.BlockPermanentEvents = true;
            Ped.RelationshipGroup = 5;
            Ped.AlwaysKeepTask = true;

            Ped.CanRagdoll = false;
            Bosses.Add(new RoamingBoss(Ped, 6, S, 0));
        }
        public void SpawnPoliceJug2(Vector3 S)
        {
            var Ped = World.CreatePed(RequestModel( PedHash.FreemodeMale01), S);
            Random RRNR = new Random();
            int CRNR = RRNR.Next(0, 800);
            #region Juggernaut
          
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 0, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 1, 91, 3, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 2, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 3, 46, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 4, 84, 6, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 5, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 6, 10, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 7, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 8, 97, 6, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 9, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 10, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 11, 186, 6, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 12, 0, 0, 1);
            
          
            #endregion

            Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, Ped, "ANIM_GROUP_MOVE_BALLISTIC", 5f);

            Function.Call(Hash.SET_PED_STRAFE_CLIPSET, Ped, "MOVE_STRAFE_BALLISTIC");
            Function.Call(Hash.SET_WEAPON_ANIMATION_OVERRIDE, Ped, 0x5534A626);
            int EnemyRelationShipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "HATES_PLAYER");



            Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, Ped, EnemyRelationShipGroup);
            Ped.RelationshipGroup = EnemyRelationShipGroup;
            Ped.IsFireProof = true;
            Ped.Weapons.Give(WeaponHash.Minigun, 9999, true, true);
            Ped.CanSufferCriticalHits = false;
            Ped.CanWrithe = false;
            Ped.AddBlip();
            Ped.Health = 4000;
            Ped.Armor = 2000;
            Ped.CurrentBlip.Sprite = BlipSprite.Juggernaut;
            Ped.CurrentBlip.Color = BlipColor.Red;
            Ped.CurrentBlip.Name = "Roaming Boss";
            Ped.Accuracy = 10;
            Ped.CurrentBlip.Alpha = 0;
            Ped.FiringPattern = FiringPattern.FullAuto;
          Ped.CanRagdoll = false;
            Bosses.Add(new RoamingBoss(Ped,3,S,0,0));
        }

        public void SpawnCadaver(Vector3 S)
        {
            var Ped = World.CreatePed(RequestModel(PedHash.FreemodeMale01), S);
            var ped = Ped;
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, 134, 0, 0);//mask
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 3, 0, 1);//gloves
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 106, 0, 1);//legs
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 152, 0, 0);//shoes
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 15, 0, 1);//shirt
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 83, 0, 1);
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 274, 0, 1);//Jacket
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 12, 0, 0, 1);
            Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, Ped, "ANIM_GROUP_MOVE_BALLISTIC", 5f);

            Function.Call(Hash.SET_PED_STRAFE_CLIPSET, Ped, "MOVE_STRAFE_BALLISTIC");
            Function.Call(Hash.SET_WEAPON_ANIMATION_OVERRIDE, Ped, 0x5534A626);
            Random RRNR = new Random();
            int CRNR = RRNR.Next(0, 800);
        
            int EnemyRelationShipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "HATES_PLAYER");



            Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, Ped, EnemyRelationShipGroup);
            Ped.RelationshipGroup = EnemyRelationShipGroup;

            Ped.Weapons.Give(WeaponHash.UnholyHellbringer, 9999, true, true);
            Ped.CanSufferCriticalHits = false;
            Ped.CanWrithe = false;
            Ped.AddBlip();
            Ped.Health = 500;
            Ped.Armor = 500;
            Ped.CurrentBlip.Sprite = BlipSprite.Juggernaut;
            Ped.CurrentBlip.Color = BlipColor.Red;
            Ped.CurrentBlip.Name = "Roaming Boss";
            Ped.Accuracy = 10;
            Ped.CurrentBlip.Alpha = 0;
            Ped.FiringPattern = FiringPattern.FullAuto;
            Ped.CanRagdoll = false;
            Bosses.Add(new RoamingBoss(Ped, 7, S, 0, 0));
        }
        public void SpawnDuplicatingBlackops (Vector3 S)
        {
            var Ped = World.CreatePed(RequestModel(PedHash.Blackops01SMY), S);


            Ped.Weapons.Give(WeaponHash.CarbineRifle, 9999, true, true);
            Ped.CanSufferCriticalHits = false;
            Ped.CanWrithe = false;
            Ped.AddBlip();
            Ped.Health = 3200;

            Ped.CurrentBlip.Sprite = BlipSprite.Juggernaut;
            Ped.CurrentBlip.Color = BlipColor.Red;
            Ped.CurrentBlip.Name = "Roaming Boss";
            Ped.CurrentBlip.Alpha = 0;
            Ped.Accuracy = 100;
            Ped.BlockPermanentEvents = true;
            Ped.RelationshipGroup = 5;
            Ped.AlwaysKeepTask = true;

            Ped.CanRagdoll = true;
            int EnemyRelationShipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "HATES_PLAYER");



            Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, Ped, EnemyRelationShipGroup);
            Ped.RelationshipGroup = EnemyRelationShipGroup;
            Bosses.Add(new RoamingBoss(Ped, 5, S, 0,20));
           
        }
        public Ped SpawnDuplicatingBlackopsSolder(Vector3 S)
        {
            var Ped = World.CreatePed(RequestModel(PedHash.Blackops01SMY), S);
            Ped.Alpha = 0;

            Ped.Weapons.Give(WeaponHash.CarbineRifle, 9999, true, true);
            Ped.CanSufferCriticalHits = false;
            Ped.CanWrithe = false;
            Ped.AddBlip();
            Ped.Health = 250;


            Ped.CurrentBlip.Sprite = BlipSprite.Juggernaut;
            Ped.CurrentBlip.Color = BlipColor.Red;
            Ped.CurrentBlip.Name = "Roaming Boss";
            Ped.CurrentBlip.Scale = 1f;
         Ped.Accuracy = 100;
            Ped.BlockPermanentEvents = true;
            Ped.RelationshipGroup = 5;
            Ped.AlwaysKeepTask = true;

            Ped.CanRagdoll = true;
            int EnemyRelationShipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "HATES_PLAYER");



            Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, Ped, EnemyRelationShipGroup);
            Ped.RelationshipGroup = EnemyRelationShipGroup;
            Ped.Task.FightAgainst(Game.Player.Character);
            return Ped;

        }
        public Ped RespawnPed(int Type, List<RoamingBoss> Boss)
        {
            Boss[Type].Boss.MarkAsNoLongerNeeded();
            Ped Ped = Boss[Type].Boss;
            if (Type == 1)
            {
                Ped = World.CreatePed(RequestModel(PedHash.FreemodeMale01), Boss[Type].StartPoint);
                Function.Call(Hash.SET_PED_PROP_INDEX, Ped, 0, 123, 0, 17);//hat
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 0, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 1, 57, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 2, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 3, 46, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 4, 84, 9, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 5, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 6, 10, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 7, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 8, 97, 9, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 9, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 10, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 11, 186, 9, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 12, 0, 0, 1);
                Function.Call(Hash.SET_PED_USING_ACTION_MODE, Ped, true, -1, 0);
                Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, Ped, "ANIM_GROUP_MOVE_BALLISTIC", 5f);

                Function.Call(Hash.SET_PED_STRAFE_CLIPSET, Ped, "MOVE_STRAFE_BALLISTIC");
                Function.Call(Hash.SET_WEAPON_ANIMATION_OVERRIDE, Ped, 0x5534A626);
                int EnemyRelationShipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "HATES_PLAYER");



                Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, Ped, EnemyRelationShipGroup);
                Ped.RelationshipGroup = EnemyRelationShipGroup;

                Ped.Weapons.Give(WeaponHash.CombatMGMk2, 9999, true, true);
                Ped.CanSufferCriticalHits = false;
                Ped.CanWrithe = false;
                Ped.AddBlip();
                Ped.Health = 4000;
                Ped.Armor = 2000;
                Ped.CurrentBlip.Sprite = BlipSprite.Juggernaut;
                Ped.CurrentBlip.Color = BlipColor.Red;
                Ped.CurrentBlip.Name = "Roaming Boss";
                Ped.Accuracy = 10;
                Ped.CurrentBlip.Alpha = 0;

                Ped.CanRagdoll = false;
            }
            if (Type == 2)
            {
                Ped = World.CreatePed(RequestModel(PedHash.Clown01SMY), Boss[Type].StartPoint);


                Ped.Weapons.Give(WeaponHash.Machete, 9999, true, true);
                Ped.CanSufferCriticalHits = false;
                Ped.CanWrithe = false;
                Ped.AddBlip();
                Ped.Health = 3000;

                Ped.CurrentBlip.Sprite = BlipSprite.Juggernaut;
                Ped.CurrentBlip.Color = BlipColor.Red;
                Ped.CurrentBlip.Name = "Roaming Boss";
                Ped.CurrentBlip.Alpha = 0;
                Ped.Accuracy = 100;
                Ped.BlockPermanentEvents = true;
                Ped.RelationshipGroup = 5;
                Ped.AlwaysKeepTask = true;

                Ped.CanRagdoll = false;
            }
            if (Type == 3)
            {
                Ped = World.CreatePed(RequestModel(PedHash.FreemodeMale01), Boss[Type].StartPoint);
                Random RRNR = new Random();
                int CRNR = RRNR.Next(0, 800);
                #region Juggernaut

                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 0, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 1, 91, 3, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 2, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 3, 46, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 4, 84, 6, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 5, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 6, 10, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 7, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 8, 97, 6, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 9, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 10, 0, 0, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 11, 186, 6, 1);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, Ped, 12, 0, 0, 1);


                #endregion

                Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, Ped, "ANIM_GROUP_MOVE_BALLISTIC", 5f);

                Function.Call(Hash.SET_PED_STRAFE_CLIPSET, Ped, "MOVE_STRAFE_BALLISTIC");
                Function.Call(Hash.SET_WEAPON_ANIMATION_OVERRIDE, Ped, 0x5534A626);
                int EnemyRelationShipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "HATES_PLAYER");



                Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, Ped, EnemyRelationShipGroup);
                Ped.RelationshipGroup = EnemyRelationShipGroup;

                Ped.Weapons.Give(WeaponHash.Minigun, 9999, true, true);
                Ped.CanSufferCriticalHits = false;
                Ped.CanWrithe = false;
                Ped.AddBlip();
                Ped.Health = 4000;
                Ped.Armor = 2000;
                Ped.CurrentBlip.Sprite = BlipSprite.Juggernaut;
                Ped.CurrentBlip.Color = BlipColor.Red;
                Ped.CurrentBlip.Name = "Roaming Boss";
                Ped.Accuracy = 10;
                Ped.CurrentBlip.Alpha = 0;
                Ped.FiringPattern = FiringPattern.FullAuto;
                Ped.CanRagdoll = false;
            }
            if (Type == 4)
            {


            }
            if (Type == 5)
            {
                Ped = World.CreatePed(RequestModel(PedHash.Blackops01SMY), Boss[Type].StartPoint);


                Ped.Weapons.Give(WeaponHash.CarbineRifle, 9999, true, true);
                Ped.CanSufferCriticalHits = false;
                Ped.CanWrithe = false;
                Ped.AddBlip();
                Ped.Health = 3200;

                Ped.CurrentBlip.Sprite = BlipSprite.Juggernaut;
                Ped.CurrentBlip.Color = BlipColor.Red;
                Ped.CurrentBlip.Name = "Roaming Boss";
                Ped.CurrentBlip.Alpha = 0;
                Ped.Accuracy = 100;
                Ped.BlockPermanentEvents = true;
                Ped.RelationshipGroup = 5;
                Ped.AlwaysKeepTask = true;

                Ped.CanRagdoll = true;
                int EnemyRelationShipGroup = Function.Call<int>(Hash.GET_HASH_KEY, "HATES_PLAYER");



                Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, Ped, EnemyRelationShipGroup);
                Ped.RelationshipGroup = EnemyRelationShipGroup;
            }
            if (Type == 6)
            {
                 Ped = World.CreatePed((Model)0x696BE0A9, Boss[Type].StartPoint);


                Ped.Weapons.Give(WeaponHash.Machete, 9999, true, true);
                Ped.CanSufferCriticalHits = false;
                Ped.CanWrithe = false;
                Ped.AddBlip();
                Ped.Health = 2900;

                Ped.CurrentBlip.Sprite = BlipSprite.Juggernaut;
                Ped.CurrentBlip.Color = BlipColor.Red;
                Ped.CurrentBlip.Name = "Roaming Boss";
                Ped.CurrentBlip.Alpha = 0;
                Ped.Accuracy = 100;
                Ped.BlockPermanentEvents = true;
                Ped.RelationshipGroup = 5;
                Ped.AlwaysKeepTask = true;

                Ped.CanRagdoll = false;
            }
                return Ped;
        }
        void DisplayHelpTextThisFrame(string text)
        {
            Function.Call(Hash._SET_TEXT_COMPONENT_FORMAT, "STRING");
            Function.Call(Hash._ADD_TEXT_COMPONENT_STRING, text);
            Function.Call(Hash._0x238FFE5C7B0498A6, 0, 0, 1, -1);
        }
        private void SetColor(int particle, float r, float g, float b, bool p1)
        {
            Function.Call(Hash.SET_PARTICLE_FX_LOOPED_COLOUR, particle, r, g, b, p1);
        }

        private void SetRange(int handle, float range)
        {
            Function.Call(Hash._SET_PARTICLE_FX_LOOPED_RANGE, handle, range);
        }
        private int GetBoneByName(Entity entity, string name)
        {
            return Function.Call<int>(Hash.GET_ENTITY_BONE_INDEX_BY_NAME, entity, name);
        }
        public void BossDefeated()
        {
            SizeF res = UIMenu.GetScreenResolutionMantainRatio();
             int middle = Convert.ToInt32(res.Width / 2);

            new Sprite("mpentry", "mp_modenotselected_gradient", new Point(0, 30), new Size(Convert.ToInt32(res.Width), 450),
                0f, Color.FromArgb(230, 255, 255, 255)).Draw();

            new UIResText("Boss Defeated!", new Point(middle, 100), 2.5f, Color.FromArgb(255, 199, 168, 87), GTA.Font.Pricedown, UIResText.Alignment.Centered).Draw();


          
            var scaleform = new Scaleform(0);

            scaleform.Load("instructional_buttons");
            //scaleform.CallFunction("CLEAR_ALL");
            //scaleform.CallFunction("TOGGLE_MOUSE_BUTTONS", 0);
            //scaleform.CallFunction("CREATE_CONTAINER");

            //scaleform.CallFunction("SET_DATA_SLOT", 0, Function.Call<string>(Hash._0x0499D7B09FC9B407, 2, (int)GTA.Control.Jump, 0), "Begin Assault");
            //scaleform.CallFunction("SET_DATA_SLOT", 1, Function.Call<string>(Hash._0x0499D7B09FC9B407, 2, (int)GTA.Control.FrontendCancel, 0), "Cancel");
            //scaleform.CallFunction("SET_DATA_SLOT", 2, Function.Call<string>(Hash._0x0499D7B09FC9B407, 2, (int)GTA.Control.PhoneRight, 0), "");
            //scaleform.CallFunction("SET_DATA_SLOT", 3, Function.Call<string>(Hash._0x0499D7B09FC9B407, 2, (int)GTA.Control.PhoneLeft, 0), "Change Mission");
            //scaleform.CallFunction("SET_DATA_SLOT", 4, Function.Call<string>(Hash._0x0499D7B09FC9B407, 2, (int)GTA.Control.FrontendRb, 0), "");
            //scaleform.CallFunction("SET_DATA_SLOT", 5, Function.Call<string>(Hash._0x0499D7B09FC9B407, 2, (int)GTA.Control.FrontendLb, 0), "Change Entry Point");
            //scaleform.CallFunction("ORBITAL_CANNON_CAM", -1);
            scaleform.Render2D();
        }
        public List<RoamingBossProp> SmokeFXprops = new List<RoamingBossProp>();
        public bool Cloaked;
        public bool StartedFight;
        public Scaleform Duf_WhiteBoard_Title;
        public Scaleform Duf_WhiteBoard_Title2;
        public Scaleform Duf_WhiteBoard_Title3;
        void OnTick(object sender, EventArgs e)
        {
            //if (Game.IsControlJustPressed(2, GTA.Control.Context))//E
            //{
            //    Game.Player.Character.Task.PlayAnimation("missrampageintrooutro", "trvram_6_1h_intro", 8f, 2500, AnimationFlags.AllowRotation);
            //}
                if (ModOn == true)
            {
                //cloak
                //scr_powerplay scr_powerplay_beast_appear
                if (firsttimeload == false)
                {
                    firsttimeload = true;
                    SpawnPoliceJug(new Vector3(1030.24f, 3088.62f, 42.1f));
                    SpawnClown(new Vector3(953.1945f, 2373.479f, 49.17f));
                    SpawnPoliceJug2(new Vector3(287.0251f, -1587.034f, 30.5f));
                    SpawnDuplicatingBlackops(new Vector3(400.38f, 782.8f, 187));
                    SpawnSlasher(new Vector3(198.8f,-934.722f,30.09f));
                    SpawnCadaver(new Vector3(247.1958f, 3588.4f, 34.1f));
                    Script.Wait(100);
                    foreach (RoamingBoss R in Bosses)
                    {
                       if(R.Boss!=null)
                        {
                            R.Boss.Task.WanderAround(R.Boss.Position, 50f);
                        }
                    }
                    }
                if (firsttimeload == true)
                {
                    if(Game.Player.Character.IsAlive==false)
                    {
                        if(Bosses.Count>0)
                        {
                            foreach (RoamingBoss R in Bosses)
                            {
                                R.Boss.Health = (int)R.SET_HP;
                                R.Boss.Armor = (int)R.SET_ARMOUR;
                                R.Boss.Position = R.StartPoint;
                                StartedFight = false;
                                R.Boss.Task.ClearAll();
                                R.Boss.Task.StandStill(9999);
                                if (R.DuplicatePeds.Count > 0)
                                {
                                    foreach (Ped PP in R.DuplicatePeds)
                                    {
                                        if (PP != null)
                                        {
                                            PP.Delete();
                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                    if(StartedFight==true)
                    {
                        foreach (RoamingBoss R in Bosses)
                        {
                            if (Function.Call<float>(Hash.GET_ENTITY_ANIM_CURRENT_TIME, Game.Player.Character, "missrampageintrooutro", "trvram_6_2h_intro") >= 0.9) //Enter
                            {
                                R.OverrideTasks = false;
                            }
                                if (World.GetDistance(Game.Player.Character.Position, R.Boss.Position) < 75)
                            {
                                //if (R.Boss.IsAlive == true)
                                //{
                                //    if (Duf_WhiteBoard_Title3 == null)
                                //    {
                                //        Duf_WhiteBoard_Title3 = new Scaleform("PLAYER_NAME_03");

                                //    }
                                //    Duf_WhiteBoard_Title3.CallFunction("SET_PLAYER_NAME", "~w~Health : ~y~" + R.Boss.Health + "~w~ / Armour : ~y~" + R.Boss.Armor);
                                //    Duf_WhiteBoard_Title3.Render3D(R.Boss.Position + new Vector3(0f, 0f, 1.8f), new Vector3(0f, 87f, (Game.Player.Character.Position - R.Boss.Position).ToHeading() - 90), new Vector3(12, 6, 4));
                                //}
                            }
                            R.Boss.IsInvincible = false;
                            R.Boss.BlockPermanentEvents = false;
                            if (R.Type == 3)
                            {
                                if (R.OutiftChangeTimer <= 500)
                                {
                                    R.OutiftChangeTimer++;
                                }
                                if (R.OutiftChangeTimer >= 500)
                                {
                                    Random RRNR = new Random();
                                    int CRNR = RRNR.Next(0, 800);
                                    #region Juggernaut
                                    if (CRNR < 100)
                                    {
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 0, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 1, 91, 3, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 2, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 3, 46, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 4, 84, 6, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 5, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 6, 10, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 7, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 8, 97, 6, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 9, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 10, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 11, 186, 6, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 12, 0, 0, 1);
                                    }
                                    if (CRNR >= 100 && CRNR <= 200)
                                    {
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 0, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 1, 91, 1, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 2, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 3, 46, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 4, 84, 1, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 5, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 6, 10, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 7, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 8, 97, 1, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 9, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 10, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 11, 186, 1, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 12, 0, 0, 1);


                                    }
                                    if (CRNR >= 200 && CRNR <= 300)
                                    {
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 0, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 1, 91, 5, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 2, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 3, 46, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 4, 84, 5, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 5, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 6, 10, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 7, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 8, 97, 5, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 9, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 10, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 11, 186, 5, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 12, 0, 0, 1);


                                    }
                                    if (CRNR >= 300 && CRNR <= 400)
                                    {
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 0, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 1, 91, 2, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 2, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 3, 46, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 4, 84, 2, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 5, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 6, 10, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 7, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 8, 97, 2, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 9, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 10, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 11, 186, 2, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 12, 0, 0, 1);

                                    }
                                    if (CRNR >= 400 && CRNR <= 500)
                                    {
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 0, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 1, 91, 4, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 2, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 3, 46, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 4, 84, 3, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 5, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 6, 10, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 7, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 8, 97, 6, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 9, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 10, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 11, 186, 3, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 12, 0, 0, 1);


                                    }
                                    if (CRNR >= 500 && CRNR <= 600)
                                    {
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 0, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 1, 91, 4, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 2, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 3, 46, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 4, 84, 4, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 5, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 6, 10, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 7, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 8, 97, 4, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 9, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 10, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 11, 186, 4, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 12, 0, 0, 1);


                                    }
                                    if (CRNR >= 600 && CRNR <= 700)
                                    {
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 0, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 1, 91, 9, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 2, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 3, 46, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 4, 84, 9, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 5, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 6, 10, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 7, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 8, 97, 9, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 9, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 10, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 11, 186, 9, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 12, 0, 0, 1);


                                    }
                                    if (CRNR >= 700 && CRNR <= 800)
                                    {
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 0, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 1, 91, 10, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 2, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 3, 46, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 4, 84, 10, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 5, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 6, 10, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 7, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 8, 97, 10, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 9, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 10, 0, 0, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 11, 186, 10, 1);
                                        Function.Call(Hash.SET_PED_COMPONENT_VARIATION, R.Boss, 12, 0, 0, 1);


                                    }
                                    #endregion
                                    R.OutiftChangeTimer = 0;
                                }
                            }
                            //  UI.ShowSubtitle("SpecialAttackTimer " + R.SpecialAttackTimer);
                            if (R.SpecialAttackTimer != 1000)
                            {
                                R.SpecialAttackTimer++;
                                if (World.GetDistance(Game.Player.Character.Position, R.Boss.Position) < 75)
                                {
                                    if (R.Type == 1)//Explosive Rounds
                                    {

                                        if (R.Boss.GetLastWeaponImpactCoords() != null)
                                        {
                                            if (R.SpecialAttackTimer > 300 && R.SpecialAttackTimer < 700)
                                            {
                                                R.Boss.Accuracy = 30;
                                                Vector3 X = R.Boss.GetLastWeaponImpactCoords();
                                                if (R.lastExplosiveCoord != X)
                                                {
                                                    R.lastExplosiveCoord = X;
                                                    World.AddOwnedExplosion(R.Boss, X, ExplosionType.VehicleBullet, 5f, 0f);
                                                }
                                            }
                                            else
                                            {
                                                R.Boss.Accuracy = 50;
                                            }
                                        }
                                    }
                                    if (R.Type == 2)
                                    {

                                        if (R.Boss.IsInCombatAgainst(Game.Player.Character) == true)
                                        {
                                            if (R.SetHp != R.Boss.Health)
                                            {
                                                R.SetHp = R.Boss.Health;
                                                Random RN = new Random();
                                                int CHN = RN.Next(0, 100);
                                                if (CHN < 75)
                                                {
                                                    R.Boss.CurrentBlip.Alpha = 0;
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    int f1 = Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "scr_clown_appears", R.Boss, 0.0f, 0.0f, 0f, 0.0f, 0.0f, 0.0f, 2.0f, false, false, false);
                                                    R.PTFX__ids.Add(f1);


                                                    Script.Wait(1000);

                                                    Vector3 X = Game.Player.Character.Position.Around(RN.Next(5, 40));
                                                    R.Boss.Position = new Vector3(X.X, X.Y, World.GetGroundHeight(X) + 1);
                                                    Script.Wait(1000);

                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    f1 = Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "scr_clown_appears", R.Boss, 0.0f, 0.0f, 0f, 0.0f, 0.0f, 0.0f, 2.0f, false, false, false);
                                                    R.PTFX__ids.Add(f1);
                                                    R.Boss.BlockPermanentEvents = true;
                                                    R.Boss.RelationshipGroup = 5;
                                                    R.Boss.AlwaysKeepTask = true;
                                                    R.Boss.Task.FightAgainst(Game.Player.Character);
                                                    R.Boss.CurrentBlip.Alpha = 255;
                                                }

                                            }
                                            #region OLD 
                                            if (R.SpecialAttackTimer == 100)
                                            {
                                                Random RN = new Random();
                                                int CHN = RN.Next(0, 100);
                                                if (CHN < 75)
                                                {
                                                    R.Boss.CurrentBlip.Alpha = 0;
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    int f1 = Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "scr_clown_appears", R.Boss, 0.0f, 0.0f, 0f, 0.0f, 0.0f, 0.0f, 2.0f, false, false, false);
                                                    R.PTFX__ids.Add(f1);


                                                    Script.Wait(1000);

                                                    Vector3 X = Game.Player.Character.Position.Around(RN.Next(5, 40));
                                                    R.Boss.Position = new Vector3(X.X, X.Y, World.GetGroundHeight(X) + 1);
                                                    Script.Wait(1000);

                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    f1 = Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "scr_clown_appears", R.Boss, 0.0f, 0.0f, 0f, 0.0f, 0.0f, 0.0f, 2.0f, false, false, false);
                                                    R.PTFX__ids.Add(f1);
                                                    R.Boss.BlockPermanentEvents = true;
                                                    R.Boss.RelationshipGroup = 5;
                                                    R.Boss.AlwaysKeepTask = true;
                                                    R.Boss.Task.FightAgainst(Game.Player.Character);
                                                    R.Boss.CurrentBlip.Alpha = 255;
                                                }

                                            }
                                            if (R.SpecialAttackTimer == 300)
                                            {
                                                Random RN = new Random();
                                                int CHN = RN.Next(0, 100);
                                                if (CHN < 75)
                                                {
                                                    R.Boss.CurrentBlip.Alpha = 0;
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    int f1 = Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "scr_clown_appears", R.Boss, 0.0f, 0.0f, 0f, 0.0f, 0.0f, 0.0f, 2.0f, false, false, false);
                                                    R.PTFX__ids.Add(f1);


                                                    Script.Wait(1000);

                                                    Vector3 X = Game.Player.Character.Position.Around(RN.Next(5, 10));
                                                    R.Boss.Position = new Vector3(X.X, X.Y, World.GetGroundHeight(X) + 1);
                                                    Script.Wait(1000);

                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    f1 = Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "scr_clown_appears", R.Boss, 0.0f, 0.0f, 0f, 0.0f, 0.0f, 0.0f, 2.0f, false, false, false);
                                                    R.PTFX__ids.Add(f1);
                                                    R.Boss.BlockPermanentEvents = true;
                                                    R.Boss.RelationshipGroup = 5;
                                                    R.Boss.AlwaysKeepTask = true;
                                                    R.Boss.Task.FightAgainst(Game.Player.Character);
                                                    R.Boss.CurrentBlip.Alpha = 255;
                                                }
                                            }
                                            if (R.SpecialAttackTimer == 500)
                                            {
                                                Random RN = new Random();
                                                int CHN = RN.Next(0, 100);
                                                if (CHN < 75)
                                                {
                                                    R.Boss.CurrentBlip.Alpha = 0;
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    int f1 = Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "scr_clown_appears", R.Boss, 0.0f, 0.0f, 0f, 0.0f, 0.0f, 0.0f, 2.0f, false, false, false);
                                                    R.PTFX__ids.Add(f1);


                                                    Script.Wait(1000);

                                                    Vector3 X = Game.Player.Character.Position.Around(RN.Next(5, 10));
                                                    R.Boss.Position = new Vector3(X.X, X.Y, World.GetGroundHeight(X) + 1);
                                                    Script.Wait(1000);

                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    f1 = Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "scr_clown_appears", R.Boss, 0.0f, 0.0f, 0f, 0.0f, 0.0f, 0.0f, 2.0f, false, false, false);
                                                    R.PTFX__ids.Add(f1);
                                                    R.Boss.BlockPermanentEvents = true;
                                                    R.Boss.RelationshipGroup = 5;
                                                    R.Boss.AlwaysKeepTask = true;
                                                    R.Boss.Task.FightAgainst(Game.Player.Character);
                                                    R.Boss.CurrentBlip.Alpha = 255;
                                                }
                                            }
                                            if (R.SpecialAttackTimer == 700)
                                            {
                                                Random RN = new Random();
                                                int CHN = RN.Next(0, 100);
                                                if (CHN < 75)
                                                {
                                                    R.Boss.CurrentBlip.Alpha = 0;
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    int f1 = Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "scr_clown_appears", R.Boss, 0.0f, 0.0f, 0f, 0.0f, 0.0f, 0.0f, 2.0f, false, false, false);
                                                    R.PTFX__ids.Add(f1);


                                                    Script.Wait(1000);

                                                    Vector3 X = Game.Player.Character.Position.Around(RN.Next(5, 10));
                                                    R.Boss.Position = new Vector3(X.X, X.Y, World.GetGroundHeight(X) + 1);
                                                    Script.Wait(1000);

                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    f1 = Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "scr_clown_appears", R.Boss, 0.0f, 0.0f, 0f, 0.0f, 0.0f, 0.0f, 2.0f, false, false, false);
                                                    R.PTFX__ids.Add(f1);
                                                    R.Boss.BlockPermanentEvents = true;
                                                    R.Boss.RelationshipGroup = 5;
                                                    R.Boss.AlwaysKeepTask = true;
                                                    R.Boss.Task.FightAgainst(Game.Player.Character);
                                                    R.Boss.CurrentBlip.Alpha = 255;
                                                }
                                            }
                                            if (R.SpecialAttackTimer == 900)
                                            {
                                                Random RN = new Random();
                                                int CHN = RN.Next(0, 100);
                                                if (CHN < 75)
                                                {


                                                    R.Boss.CurrentBlip.Alpha = 0;
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    int f1 = Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "scr_clown_appears", R.Boss, 0.0f, 0.0f, 0f, 0.0f, 0.0f, 0.0f, 2.0f, false, false, false);
                                                    R.PTFX__ids.Add(f1);


                                                    Script.Wait(1000);

                                                    Vector3 X = Game.Player.Character.Position.Around(RN.Next(5, 10));
                                                    R.Boss.Position = new Vector3(X.X, X.Y, World.GetGroundHeight(X) + 1);
                                                    Script.Wait(1000);

                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry2");
                                                    Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry2");
                                                    f1 = Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "scr_clown_appears", R.Boss, 0.0f, 0.0f, 0f, 0.0f, 0.0f, 0.0f, 2.0f, false, false, false);
                                                    R.PTFX__ids.Add(f1);
                                                    R.Boss.BlockPermanentEvents = true;
                                                    R.Boss.RelationshipGroup = 5;
                                                    R.Boss.AlwaysKeepTask = true;
                                                    R.Boss.Task.FightAgainst(Game.Player.Character);
                                                    R.Boss.CurrentBlip.Alpha = 255;
                                                }
                                            }
                                            #endregion
                                        }
                                    }
                                    if (R.Type == 7)//GasRounds Rounds
                                    {

                                        if (SmokeFXprops.Count > 0)
                                        {
                                            try
                                            {
                                                foreach (RoamingBossProp RBP in SmokeFXprops)
                                                {
                                                    if (RBP.Prop != null)
                                                    {
                                                        if (World.GetDistance(Game.Player.Character.Position, RBP.Prop.Position) < 3f)
                                                        {
                                                            Function.Call(Hash.PLAY_PAIN, Game.Player.Character, 8,0,0);
                                                            Game.Player.Character.Health -= 5;
                                                            Function.Call(Hash.STOP_PARTICLE_FX_LOOPED, RBP.ParticleFX1, false);
                                                            if (RBP.Prop != null)
                                                            {
                                                                RBP.Prop.Delete();
                                                            }

                                                            SmokeFXprops.Remove(RBP);
                                                        }
                                                        RBP.Prop.FreezePosition = true;
                                                    }
                                                    if (RBP.Timer <= 1000)
                                                    {
                                                      //  GTA.World.DrawMarker(MarkerType.DebugSphere, RBP.Prop.Position, Vector3.Zero, Vector3.Zero, new Vector3(0.35f, 0.35f, 0.35f), Color.FromArgb(0, 147, 255));
                                                        RBP.Timer++;
                                                    }
                                                    if (RBP.Timer >= 1000)
                                                    {
                                                        Function.Call(Hash.STOP_PARTICLE_FX_LOOPED, RBP.ParticleFX1, false);
                                                        if (RBP.Prop != null)
                                                        {
                                                            RBP.Prop.Delete();
                                                        }

                                                        SmokeFXprops.Remove(RBP);
                                                    }
                                                }
                                            }
                                            catch
                                            {

                                            }
                                        }
                                     
                                            if (R.SpecialAttackTimer > 200 && R.SpecialAttackTimer < 900)
                                            {
                                                R.Boss.Accuracy = 30;
                                                Random r = new Random();
                                                int C = r.Next(2, 20);
                                                int CHN = r.Next(0, 100);
                                                if (R.SpecialAttackTimer == 250)
                                                {
                                                    R.OverrideTasks = true;
                                                    R.Boss.Task.PlayAnimation("missrampageintrooutro", "trvram_6_2h_intro", 8f, 3500, AnimationFlags.AllowRotation);

                                                    R.OutiftChangeTimer = R.SpecialAttackTimer + r.Next(75, 300);
                                                    if (CHN < 80)
                                                    {
                                                        for (int i = 0; i < C; i++)
                                                        {


                                                            Vector3 X = Game.Player.Character.Position.Around(r.Next(1, 5));
                                                            X = new Vector3(X.X, X.Y, World.GetGroundHeight(X) + 0.25f);
                                                            Prop P = World.CreateProp(RequestModel("prop_flare_01b"), X, false, false);
                                                            P.Alpha = 0;
                                                            P.IsVisible = false;
                                                            P.HasCollision = false;
                                                            Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry1");
                                                            Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry1");
                                                            Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry1");
                                                            Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry1");
                                                            int f1 = Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "scr_alien_teleport", P, 0.0f, 0.0f, 0f, 0.0f, 0.0f, 0.0f, 3.0f, false, false, false);

                                                            R.PTFX__ids.Add(f1);
                                                            SetColor(f1, 255, 255, 255, true);
                                                            SmokeFXprops.Add(new RoamingBossProp(P, f1, 0));
                                                            Script.Wait(1);
                                                        }
                                                    }
                                                }
                                                if (R.SpecialAttackTimer == R.OutiftChangeTimer)
                                            {
                                                R.OverrideTasks = true;
                                                R.Boss.Task.PlayAnimation("missrampageintrooutro", "trvram_6_2h_intro", 8f, 3500, AnimationFlags.AllowRotation);
                                                R.OutiftChangeTimer = R.SpecialAttackTimer + r.Next(75, 300);
                                                            if (CHN < 80)
                                                    {
                                                        for (int i = 0; i < C; i++)
                                                        {


                                                            Vector3 X = Game.Player.Character.Position.Around(r.Next(2, 10));
                                                            X = new Vector3(X.X, X.Y, World.GetGroundHeight(X) + 0.25f);
                                                            Prop P = World.CreateProp(RequestModel("prop_flare_01b"), X, false, false);
                                                            P.Alpha = 0;
                                                            P.IsVisible = false;
                                                            P.HasCollision = false;
                                                            Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry1");
                                                            Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry1");
                                                            Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_rcbarry1");
                                                            Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_rcbarry1");
                                                            int f1 = Function.Call<int>(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "scr_alien_teleport", P, 0.0f, 0.0f, 0f, 0.0f, 0.0f, 0.0f, 1.0f, false, false, false);

                                                            R.PTFX__ids.Add(f1);
                                                            SetColor(f1, 255, 255, 255, true);
                                                            SmokeFXprops.Add(new RoamingBossProp(P, f1, 0));
                                                            Script.Wait(1);
                                                        }
                                                    }

                                                }
                                            
                                              
                                                   
                                                
                                            }
                                            else
                                            {
                                                R.Boss.Accuracy = 50;
                                            }
                                        
                                    }
                                    if (R.Type == 5)//Duplicating Peds
                                    {
                                        if (R.Boss.IsAlive == false)
                                        {
                                            if (R.DuplicatePeds.Count > 0)
                                            {
                                                foreach (Ped PP in R.DuplicatePeds)
                                                {
                                                    if (PP != null)
                                                    {
                                                        PP.Delete();
                                                    }
                                                }
                                            }
                                        }
                                        if (R.DuplicatePeds.Count > 0)
                                        {
                                            foreach (Ped PP in R.DuplicatePeds)
                                            {
                                                if (PP.IsAlive == false)
                                                {
                                                    PP.CurrentBlip.Alpha = 0;
                                                }
                                            }
                                        }

                                        if (R.SpecialAttackTimer == R.OutiftChangeTimer)
                                        {
                                            if (R.WaitingForSpawn == false)
                                            {
                                                Ped P = SpawnDuplicatingBlackopsSolder(R.Boss.Position.Around(10));
                                                R.DuplicatePeds.Add(P);
                                                R.WaitingForSpawn = true;
                                                P.Alpha = 0;
                                                Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_xm_heat");
                                                Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_xm_heat");
                                                Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_xm_heat");
                                                Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_xm_heat");
                                                Bone[] Bones = (Bone[])Enum.GetValues(typeof(Bone));
                                                foreach (Bone B in Bones)
                                                {
                                                    Ped p = P;
                                                    int f1 = Function.Call<int>(Hash.START_PARTICLE_FX_LOOPED_ON_PED_BONE, "scr_xm_heat_camo", P,
                                                        0.0f, 0.0f, 0f, 90f, 180f, 0.0f, (int)B, 0.5f, false, false, false);
                                                    PTFX__ids.Add(f1);
                                                    f1 = Function.Call<int>(Hash.START_PARTICLE_FX_LOOPED_ON_PED_BONE, "scr_xm_heat_camo", P,
                                                        0.0f, 0.0f, 0f, 90f, 180f, 0.0f, (int)B, 0.5f, false, false, false);
                                                    PTFX__ids.Add(f1);
                                                }
                                                R.CurrentPedSpawned = P;
                                                R.OutiftChangeTimer = R.OutiftChangeTimer + 300;
                                            }
                                        }
                                        if (R.SpecialAttackTimer > 990 && R.SpecialAttackTimer < 999)
                                        {
                                            Random RNR = new Random();
                                            R.OutiftChangeTimer = RNR.Next(0, 100);
                                        }
                                        if (R.SpecialAttackTimer == R.OutiftChangeTimer)
                                        {
                                            if (R.WaitingForSpawn == true)
                                            {

                                                int F = 0;

                                                for (int i = 0; i < 256; i++)
                                                {

                                                    R.CurrentPedSpawned.Alpha = i;
                                                    if (R.CurrentPedSpawned.Weapons.CurrentWeaponObject != null)
                                                    {
                                                        R.CurrentPedSpawned.Weapons.CurrentWeaponObject.Alpha = i;
                                                    }
                                                    F = 0;

                                                    if (F >= 255)
                                                    {
                                                        F = 255;
                                                        break;
                                                    }
                                                    if (F <= 255)
                                                    {
                                                        F = F + 15;
                                                    }
                                                }
                                                foreach (int I in R.PTFX__ids)
                                                {
                                                    Function.Call(Hash.STOP_PARTICLE_FX_LOOPED, I, false);
                                                }
                                                R.CurrentPedSpawned.Alpha = 255;
                                                R.WaitingForSpawn = false;
                                                Random RNR = new Random();
                                                R.OutiftChangeTimer += 200;
                                                R.CurrentPedSpawned = null;
                                            }
                                        }


                                    }
                                    if (R.Type == 6)
                                    {
                                        if (R.Boss.IsAlive == true)
                                        {
                                            if (World.GetDistance(R.Boss.Position, R.MovePoint) > 10)
                                            {
                                                if (R.OverrideTasks == true)
                                                {
                                               
                                                    R.OverrideTasks = false;
                                                    R.Boss.CurrentBlip.Alpha = 255;
                                                    int F = 0;
                                                    for (int i = 0; i < 256; i++)
                                                    {

                                                        R.Boss.Alpha = i;
                                                        if (R.Boss.Weapons.CurrentWeaponObject != null)
                                                        {
                                                            R.Boss.Weapons.CurrentWeaponObject.Alpha = i;
                                                        }
                                                        F = 0;

                                                        if (F >= 255)
                                                        {
                                                            F = 255;
                                                            break;
                                                        }
                                                        if (F <= 255)
                                                        {
                                                            F = F + 15;
                                                        }
                                                    }
                                                    foreach (int I in R.PTFX__ids)
                                                    {
                                                        Function.Call(Hash.STOP_PARTICLE_FX_LOOPED, I, false);
                                                    }
                                                }
                                            }
                                            if (R.SpecialAttackTimer == 50)
                                            {
                                                Vector3 X = Game.Player.Character.Position.Around(15);
                                                R.MovePoint = new Vector3(X.X, X.Y, World.GetGroundHeight(X) + 1);
                                                // R.Boss.Task.RunTo(R.MovePoint, false);
                                              
                                             
                                              
                                                Random RN = new Random();
                                                R.OutiftChangeTimer = RN.Next(50, 100);
                                                R.Boss.CurrentBlip.Alpha = 0;
                                                R.Boss.CurrentBlip.Alpha = 0;
                                                #region Camo
                                                Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_xm_heat");
                                                Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_xm_heat");
                                                Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_xm_heat");
                                                Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_xm_heat");
                                                Bone[] Bones = (Bone[])Enum.GetValues(typeof(Bone));
                                                foreach (Bone B in Bones)
                                                {
                                                    Ped P = R.Boss;
                                                    int f1 = Function.Call<int>(Hash.START_PARTICLE_FX_LOOPED_ON_PED_BONE, "scr_xm_heat_camo", P,
                                                        0.0f, 0.0f, 0f, 90f, 180f, 0.0f, (int)B, 0.5f, false, false, false);
                                                    R.PTFX__ids.Add(f1);
                                                    f1 = Function.Call<int>(Hash.START_PARTICLE_FX_LOOPED_ON_PED_BONE, "scr_xm_heat_camo", P,
                                                        0.0f, 0.0f, 0f, 90f, 180f, 0.0f, (int)B, 0.5f, false, false, false);
                                                    R.PTFX__ids.Add(f1);
                                                }
                                                int F = 255;
                                                for (int i = 255; i > 0; i--)
                                                {

                                                    F = F - 15;
                                                    if (F <= 0)
                                                    {
                                                        F = 0;
                                                    }
                                                    Script.Yield();
                                                    R.Boss.Alpha = F;
                                                    if (R.Boss.Weapons.CurrentWeaponObject != null)
                                                    {
                                                        R.Boss.Weapons.CurrentWeaponObject.Alpha = F;
                                                    }
                                                    if (F <= 0)
                                                    {
                                                        F = 0;
                                                        break;
                                                    }


                                                }
                                                R.Boss.Position = R.MovePoint;
                                                R.OverrideTasks = true;
                                                R.Boss.Task.FightAgainst(Game.Player.Character);
                                                #endregion

                                            }
                                            if (R.SpecialAttackTimer == R.OutiftChangeTimer)
                                            {
                                                Vector3 X = Game.Player.Character.Position.Around(15);
                                                R.MovePoint = new Vector3(X.X, X.Y, World.GetGroundHeight(X) + 1);
                                                // R.Boss.Task.RunTo(R.MovePoint, false);



                                                Random RN = new Random();
                                                R.OutiftChangeTimer = RN.Next(50, 100);
                                                R.Boss.CurrentBlip.Alpha = 0;
                                                R.Boss.CurrentBlip.Alpha = 0;
                                                #region Camo
                                                Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_xm_heat");
                                                Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_xm_heat");
                                                Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, "scr_xm_heat");
                                                Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, "scr_xm_heat");
                                                Bone[] Bones = (Bone[])Enum.GetValues(typeof(Bone));
                                                foreach (Bone B in Bones)
                                                {
                                                    Ped P = R.Boss;
                                                    int f1 = Function.Call<int>(Hash.START_PARTICLE_FX_LOOPED_ON_PED_BONE, "scr_xm_heat_camo", P,
                                                        0.0f, 0.0f, 0f, 90f, 180f, 0.0f, (int)B, 0.5f, false, false, false);
                                                    R.PTFX__ids.Add(f1);
                                                    f1 = Function.Call<int>(Hash.START_PARTICLE_FX_LOOPED_ON_PED_BONE, "scr_xm_heat_camo", P,
                                                        0.0f, 0.0f, 0f, 90f, 180f, 0.0f, (int)B, 0.5f, false, false, false);
                                                    R.PTFX__ids.Add(f1);
                                                }
                                                int F = 255;
                                                for (int i = 255; i > 0; i--)
                                                {

                                                    F = F - 15;
                                                    if (F <= 0)
                                                    {
                                                        F = 0;
                                                    }
                                                    Script.Yield();
                                                    R.Boss.Alpha = F;
                                                    if (R.Boss.Weapons.CurrentWeaponObject != null)
                                                    {
                                                        R.Boss.Weapons.CurrentWeaponObject.Alpha = F;
                                                    }
                                                    if (F <= 0)
                                                    {
                                                        F = 0;
                                                        break;
                                                    }


                                                }
                                                R.Boss.Position = R.MovePoint;
                                                R.OverrideTasks = true;
                                                R.Boss.Task.FightAgainst(Game.Player.Character);
                                                #endregion
                                            }
                                        }

                                    }
                                }
                            }
                            if (R.SpecialAttackTimer == 1000)
                            {
                                if (R.Type == 5)
                                {

                                }

                                R.SpecialAttackTimer = 0;
                            }



                            if (R.Timer != 210)
                            {
                                R.Timer++;

                            }

                            if (R.Timer == 210)
                            {
                                if (R.Boss.IsWalking == false && R.Boss.IsInCombat == false)
                                {
                                    R.WaitingForStop = false;
                                }
                                R.Timer = 0;
                            }

                            if (R.Boss.IsWalking == true && R.Boss.IsInCombat == false)
                            {
                                // UI.ShowSubtitle("Moving "+ R.Waypoint);
                                R.WaitingForStop = false;
                            }
                            if (R.Boss.IsWalking == false && R.Boss.IsInCombat == false)
                            {

                                if (R.WaitingForStop == false)
                                {

                                    Random RN = new Random();
                                    Vector3 L = R.StartPoint.Around(RN.Next(100, 1300));
                                    if (R.Waypoint != L)
                                    {
                                        R.Waypoint = L;

                                    }
                                    // UI.ShowSubtitle("Stopped "+ R.Waypoint);
                                    R.WaitingForStop = true;
                                    R.Boss.Task.GoTo(R.Waypoint, true, -1);

                                }


                            }



                            if (World.GetDistance(Game.Player.Character.Position, R.Boss.Position) < 75)
                            {
                                if (R.Boss.IsAlive == true)
                                {
                                    if (R.Boss.IsInCombatAgainst(Game.Player.Character) == false)
                                    {
                                        if (R.OverrideTasks == false)
                                        {
                                            R.Boss.Task.FightAgainst(Game.Player.Character);
                                        }

                                    }

                                }
                            }
                            if (World.GetDistance(Game.Player.Character.Position, R.Boss.Position) < 150F)
                            {
                                if (R.Boss.IsAlive == true)
                                {
                                    if (R.OverrideTasks == false)
                                    {
                                        R.Boss.CurrentBlip.Alpha = 255;
                                    }
                                }
                                if (R.Boss.IsAlive == false)
                                {
                                    R.Boss.CurrentBlip.Alpha = 0;
                                    if (R.Boss.Alpha == 255)
                                    {
                                        R.Boss.Alpha = 254;
                                        for (int i = 0; i < 500; i++)
                                        {

                                            BossDefeated();
                                            Script.Wait(1);
                                        }
                                        StartedFight = false;

                                    }
                                }
                            }
                            if (World.GetDistance(Game.Player.Character.Position, R.Boss.Position) > 150F)
                            {
                                R.Boss.CurrentBlip.Alpha = 0;
                            }
                        }
                    }
                    if (StartedFight == false)
                    {
                        foreach (RoamingBoss R in Bosses)
                        {
                            if (World.GetDistance(Game.Player.Character.Position, R.Boss.Position) > 75)
                            {
                                //if (R.Boss.IsAlive == false)
                                //{
                                //    if (Bosses[0].Boss == R.Boss)
                                //    {
                                //        RespawnPed(1, Bosses);
                                //    }
                                //    if (Bosses[1].Boss == R.Boss)
                                //    {
                                //        RespawnPed(2, Bosses);
                                //    }
                                //    if (Bosses[2].Boss == R.Boss)
                                //    {
                                //        RespawnPed(3, Bosses);

                                //    }

                                //    if (Bosses[4].Boss == R.Boss)
                                //    {
                                //        RespawnPed(5, Bosses);
                                //    }
                                //    if (Bosses[5].Boss == R.Boss)
                                //    {
                                //        RespawnPed(6, Bosses);

                                //    }
                                //}
                            }
                            if (R.Boss.IsAlive == true)
                            {
                                if (World.GetDistance(Game.Player.Character.Position, R.Boss.Position) < 500f)
                                {
                                    Vector3 X = new Vector3(R.Boss.Position.X, R.Boss.Position.Y, R.Boss.Position.Z - 1f);
                                    GTA.World.DrawMarker(MarkerType.VerticalCylinder, X,
                                    Vector3.Zero, Vector3.Zero, new Vector3(6f, 6f, 0.7f), System.Drawing.Color.FromArgb(255, 0, 125, 255));
                                    if (World.GetDistance(Game.Player.Character.Position, R.Boss.Position) < 10f)
                                    {

                                        DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to begin Boss Fight");
                                        if (Game.IsControlJustPressed(2, GTA.Control.Context))//E
                                        {
                                            R.Boss.Task.PlayAnimation("missrampageintrooutro", "trvram_6_1h_intro", 8f, 3500, AnimationFlags.AllowRotation);
                                            Script.Wait(3000);
                                            Game.FadeScreenOut(500);
                                            Script.Wait(500);
                                            Game.Player.Character.Position = World.GetNextPositionOnSidewalk(Game.Player.Character.Position.Around(120));
                                            Script.Wait(500);
                                            Game.FadeScreenIn(500);
                                            StartedFight = true;

                                        }
                                    }
                                    if (World.GetDistance(Game.Player.Character.Position, R.Boss.Position) < 200f)
                                    {

                                        if (Duf_WhiteBoard_Title == null)
                                        {
                                            Duf_WhiteBoard_Title = new Scaleform("PLAYER_NAME_08");
                                        }
                                        if (Duf_WhiteBoard_Title2 == null)
                                        {
                                            Duf_WhiteBoard_Title2 = new Scaleform("PLAYER_NAME_09");
                                        }
                                        if (R.Type == 1)
                                        {
                                            Duf_WhiteBoard_Title.CallFunction("SET_PLAYER_NAME", "~r~Attack/Ability : Explosive Ammo ~w~");
                                        }
                                        if (R.Type == 2)
                                        {
                                            Duf_WhiteBoard_Title.CallFunction("SET_PLAYER_NAME", "~r~Attack/Ability : Teleport~w~");
                                        }
                                        if (R.Type == 3)
                                        {
                                            Duf_WhiteBoard_Title.CallFunction("SET_PLAYER_NAME", "~r~Attack/Ability : Minigun~w~");

                                        }

                                        if (R.Type == 5)
                                        {
                                            Duf_WhiteBoard_Title.CallFunction("SET_PLAYER_NAME", "~r~Attack/Ability : Duplicate Self ~w~");
                                        }
                                        if (R.Type == 6)
                                        {
                                            Duf_WhiteBoard_Title.CallFunction("SET_PLAYER_NAME", "~r~Attack/Ability : Invisibility Cloak~w~");

                                        }
                                        if (R.Type == 7)
                                        {
                                            Duf_WhiteBoard_Title.CallFunction("SET_PLAYER_NAME", "~r~Attack/Ability : Acid Rain~w~");

                                        }

                                        Duf_WhiteBoard_Title2.CallFunction("SET_PLAYER_NAME", "~w~Health : ~y~" + R.Boss.Health + "~w~ / Armour : ~y~" + R.Boss.Armor);
                                        Duf_WhiteBoard_Title.Render3D(X + new Vector3(0f, 0f, 4f), new Vector3(0f, 87f, (Game.Player.Character.Position - R.Boss.Position).ToHeading() - 90), new Vector3(12, 6, 4));
                                        Duf_WhiteBoard_Title2.Render3D(X + new Vector3(0f, 0f, 2.8f), new Vector3(0f, 87f, (Game.Player.Character.Position - R.Boss.Position).ToHeading() - 90), new Vector3(12, 6, 4));
                                        R.Boss.CurrentBlip.Alpha = 255;

                                    }
                                    if (World.GetDistance(Game.Player.Character.Position, R.Boss.Position) > 200f)
                                    {
                                        R.Boss.CurrentBlip.Alpha = 0;

                                    }
                                    R.Boss.IsInvincible = true;
                                    R.Boss.BlockPermanentEvents = true;
                                }
                            }
                        }
                    }
                }

            }
        }
        private void OnKeyDown()
        {

        }
        public static string LoadDict(string dict)
        {
            while (!Function.Call<bool>(Hash.HAS_ANIM_DICT_LOADED, dict))
            {
                Function.Call(Hash.REQUEST_ANIM_DICT, dict);
                Script.Yield();
            }

            return dict;
        }
        private void OnShutdown(object sender, EventArgs e)
        {
            var A_0 = true;
            if (A_0)
            {
                foreach (RoamingBossProp RBP in SmokeFXprops)
                {
                    if (RBP.Prop != null)
                    {
                        RBP.Prop.Delete();
                    }
                    Function.Call(Hash.STOP_PARTICLE_FX_LOOPED, RBP.ParticleFX1, false);

                    
                }
                foreach (int I in PTFX__ids)
                {
                    Function.Call(Hash.STOP_PARTICLE_FX_LOOPED, I, false);
                }
                foreach (RoamingBoss R in Bosses)
                {
                 
                   
                    foreach (int I in R.PTFX__ids)
                    {
                        Function.Call(Hash.STOP_PARTICLE_FX_LOOPED,I, false);
                    }
                    if (R.Boss != null)
                    {
                        R.Boss.Delete();
                    }
                    if (R.DuplicatePeds.Count > 0)
                    {
                        foreach (Ped PP in R.DuplicatePeds)
                        {
                            if (PP != null)
                            {
                                PP.Delete();
                            }
                        }
                    }
                }
            }
        }
            }
}
