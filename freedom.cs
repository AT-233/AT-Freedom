using SPT.Reflection.Patching;
using AmplifyImpostors;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using Comfort.Common;
using EFT;
using EFT.Ballistics;
using EFT.InventoryLogic;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static EFT.Player;
using static GripPose;
using Object = UnityEngine.Object;

namespace AT.freedom
{
    [BepInPlugin("AT.freedom", "AT.freedom", "1.0.0.0")]
    public class freedomcore : BaseUnityPlugin
    {
        public void Awake()
        {
            Console.WriteLine("自由加载完毕");
        }

        void Start()
        {

        }
        void Update()
        {

        }

    }
    public class freedom : MonoBehaviour
    {
        public AudioClip[] shoot;
        public AudioClip[] change;
        public GameObject shotgunmode;
        public GameObject snipermode;
        public GameObject ammoimage;
        public Material ammoimagematerial;
        private Material[] newmaterial;
        public bool gunmode = false;
        public static bool snipershot = false;
        public static bool shotgunshot = false;
        public static GameObject shotgunmode1;
        public static GameObject snipermode1;
        public static GameWorld gameWorld;
        public static Player.FirearmController NowFirearm;
        // Start is called before the first frame update
        void Start()
        {
            new freedomshot().Enable();
            shotgunmode1 = shotgunmode;
            snipermode1 = snipermode;
            newmaterial = new Material[ammoimage.GetComponent<MeshRenderer>().materials.Length];//替换材质只能这样搞
            newmaterial[0] = ammoimagematerial;
            
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (gameWorld == null)
            {
                gameWorld = Singleton<GameWorld>.Instance;
                return;
            }
            if (gameWorld != null && NowFirearm == null)
            {
                NowFirearm = gameWorld.MainPlayer.HandsController as FirearmController;
                return;
            }
            if (NowFirearm != null && NowFirearm.Weapon.TemplateId == "freedom")
            {
                if (shotgunmode.activeSelf && NowFirearm.Weapon.Chambers[0].ID != "0002aaaaaaa1145141919810freedomstbullet" && NowFirearm.Weapon.ChamberAmmoCount != 0)
                {
                    NowFirearm.Weapon.Chambers[0].RemoveItem(false);
                    NowFirearm.Weapon.Chambers[0].Add(CreateItem<Item>("0002aaaaaaa1145141919810freedomstbullet", null), false, false);
                }
                if (snipermode.activeSelf && NowFirearm.Weapon.Chambers[0].ID != "0001aaaaaaa1145141919810freedomapbullet" && NowFirearm.Weapon.ChamberAmmoCount != 0)
                {
                    NowFirearm.Weapon.Chambers[0].RemoveItem(false);
                    NowFirearm.Weapon.Chambers[0].Add(CreateItem<Item>("0001aaaaaaa1145141919810freedomapbullet", null), false, false);
                }
                if (shotgunmode.activeSelf != gunmode && shotgunmode.activeSelf)
                {
                    gunmode = shotgunmode.activeSelf;
                    GetComponent<AudioSource>().clip = change[0];
                    GetComponent<AudioSource>().Play();
                }
                if (shotgunmode.activeSelf != gunmode && !shotgunmode.activeSelf)
                {
                    gunmode = shotgunmode.activeSelf;
                    GetComponent<AudioSource>().clip = change[1];
                    GetComponent<AudioSource>().Play();
                }
                if (snipershot)
                {
                    GetComponent<AudioSource>().clip = shoot[1];
                    GetComponent<AudioSource>().Play();
                    snipershot = false;
                }
                if (shotgunshot)
                {
                    GetComponent<AudioSource>().clip = shoot[0];
                    GetComponent<AudioSource>().Play();
                    shotgunshot = false;
                }
                if (NowFirearm.Weapon.GetCurrentMagazine() != null)
                {
                    int count = NowFirearm.Weapon.GetCurrentMagazineCount() + NowFirearm.Weapon.ChamberAmmoCount;
                    if (count > 13)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.yellow);
                    }
                    if (count == 13)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.green);
                        ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0, 0);
                    }
                    if (count == 12)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                        ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0, 0);
                    }
                    if (count == 11)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                        ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0.0833f, 0);
                    }
                    if (count == 10)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                        ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0.1666f, 0);
                    }
                    if (count == 9)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                        ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0.25f, 0);
                    }
                    if (count == 8)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                        ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0.3333f, 0);
                    }
                    if (count == 7)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                        ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0.4166f, 0);
                    }
                    if (count == 6)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                        ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0.5f, 0);
                    }
                    if (count == 5)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                        ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0.5833f, 0);
                    }
                    if (count == 4)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                        ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0.6666f, 0);
                    }
                    if (count == 3)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                        ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0.75f, 0);
                    }
                    if (count == 2)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                        ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0.8333f, 0);
                    }
                    if (count == 1)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                        ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0.9166f, 0);
                    }
                    if (count == 0)
                    {
                        ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.white);
                        ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(1, 0);
                    }
                }
                if (NowFirearm.Weapon.GetCurrentMagazine() == null && NowFirearm.Weapon.ChamberAmmoCount != 0)
                {
                    ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.blue);
                    ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0, 0);
                }
                if (NowFirearm.Weapon.GetCurrentMagazine() == null && NowFirearm.Weapon.ChamberAmmoCount == 0)
                {
                    ammoimage.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.red);
                    ammoimage.GetComponent<MeshRenderer>().materials[0].mainTextureOffset = new Vector2(0, 0);
                }
            }
        }
        public static T CreateItem<T>(string templateId, string itemId = null) where T : Item
        {
            bool instantiated = Singleton<ItemFactory>.Instantiated;
            if (instantiated)
            {
                ItemFactory instance = Singleton<ItemFactory>.Instance;
                bool flag = instance.ItemTemplates.ContainsKey(templateId);
                if (flag)
                {
                    bool flag2 = itemId == null;
                    if (flag2)
                    {
                        itemId = GenerateId();
                    }
                    return instance.CreateItem(itemId, templateId, null) as T;
                }
            }
            return default(T);
        }
        public static string GenerateId()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 24);
        }

    }
    internal class freedomshot : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BallisticsCalculator).GetMethod("Shoot");
        }

        [PatchPostfix]
        private static void Postfix(EftBulletClass shot)
        {
            Weapon weapon = null;
            bool flag;
            if (shot.PlayerProfileID == freedom.gameWorld.MainPlayer.ProfileId)
            {
                weapon = (shot.Weapon as Weapon);
                flag = (weapon != null && weapon.TemplateId == "freedom");
            }
            else
            {
                flag = false;
            }
            bool flag2 = flag;
            if (flag2)
            {
                if (freedom.shotgunmode1.activeSelf)
                {
                    freedom.shotgunshot = true;
                }
                if (freedom.snipermode1.activeSelf)
                {
                    freedom.snipershot = true;
                }
            }
        }
    }
}
