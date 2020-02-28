using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using PsychoticLab;

namespace Procedural03
{
    //
    public enum Gender
    {
        Male,
        Female
    }

    public enum ClassChoose
    {
        Warrior,
        Rogue,
        Mage,
        Priest,
        Berserk,
        None
    }

    public enum Race { Human, Elf }
    public enum SkinColor { White, Brown, Black, Elf }
    public enum Elements { Yes, No }
    public enum TypeHeadCovering { HeadCoverings_Base_Hair, HeadCoverings_No_FacialHair, HeadCoverings_No_Hair }
    public enum FacialHair { Yes, No }

    public class CharacterGenerator : MonoBehaviour
    {
        SeedGenerator seedGenerator;

        CharacterObjectGroups male = new CharacterObjectGroups();
        CharacterObjectGroups female = new CharacterObjectGroups();
        CharacterObjectListsAllGender allGender = new CharacterObjectListsAllGender();

        int seed;
        int seedEnhancer;

        //Declaring variable for the player
        [Header("Material")]
        public Material mat;

        [Header("Gear Colors")]
        public Color[] primary = { new Color(0.2862745f, 0.4f, 0.4941177f), new Color(0.4392157f, 0.1960784f, 0.172549f), new Color(0.3529412f, 0.3803922f, 0.2705882f), new Color(0.682353f, 0.4392157f, 0.2196079f), new Color(0.4313726f, 0.2313726f, 0.2705882f), new Color(0.5921569f, 0.4941177f, 0.2588235f), new Color(0.482353f, 0.4156863f, 0.3529412f), new Color(0.2352941f, 0.2352941f, 0.2352941f), new Color(0.2313726f, 0.4313726f, 0.4156863f) };
        public Color[] secondary = { new Color(0.7019608f, 0.6235294f, 0.4666667f), new Color(0.7372549f, 0.7372549f, 0.7372549f), new Color(0.1647059f, 0.1647059f, 0.1647059f), new Color(0.2392157f, 0.2509804f, 0.1882353f) };

        [Header("Metal Colors")]
        public Color[] metalPrimary = { new Color(0.6705883f, 0.6705883f, 0.6705883f), new Color(0.5568628f, 0.5960785f, 0.6392157f), new Color(0.5568628f, 0.6235294f, 0.6f), new Color(0.6313726f, 0.6196079f, 0.5568628f), new Color(0.6980392f, 0.6509804f, 0.6196079f) };
        public Color[] metalSecondary = { new Color(0.3921569f, 0.4039216f, 0.4117647f), new Color(0.4784314f, 0.5176471f, 0.5450981f), new Color(0.3764706f, 0.3607843f, 0.3372549f), new Color(0.3254902f, 0.3764706f, 0.3372549f), new Color(0.4f, 0.4039216f, 0.3568628f) };

        [Header("Leather Colors")]
        public Color[] leatherPrimary;
        public Color[] leatherSecondary;

        [Header("Skin Colors")]
        public Color[] whiteSkin = { new Color(1f, 0.8000001f, 0.682353f) };
        public Color[] brownSkin = { new Color(0.8196079f, 0.6352941f, 0.4588236f) };
        public Color[] blackSkin = { new Color(0.5647059f, 0.4078432f, 0.3137255f) };
        public Color[] elfSkin = { new Color(0.9607844f, 0.7843138f, 0.7294118f) };

        [Header("Hair Colors")]
        public Color[] whiteHair = { new Color(0.3098039f, 0.254902f, 0.1764706f), new Color(0.2196079f, 0.2196079f, 0.2196079f), new Color(0.8313726f, 0.6235294f, 0.3607843f), new Color(0.8901961f, 0.7803922f, 0.5490196f), new Color(0.8000001f, 0.8196079f, 0.8078432f), new Color(0.6862745f, 0.4f, 0.2352941f), new Color(0.5450981f, 0.427451f, 0.2156863f), new Color(0.8470589f, 0.4666667f, 0.2470588f) };
        public Color whiteStubble = new Color(0.8039216f, 0.7019608f, 0.6313726f);
        public Color[] brownHair = { new Color(0.3098039f, 0.254902f, 0.1764706f), new Color(0.1764706f, 0.1686275f, 0.1686275f), new Color(0.3843138f, 0.2352941f, 0.0509804f), new Color(0.6196079f, 0.6196079f, 0.6196079f), new Color(0.6196079f, 0.6196079f, 0.6196079f) };
        public Color brownStubble = new Color(0.6588235f, 0.572549f, 0.4627451f);
        public Color[] blackHair = { new Color(0.2431373f, 0.2039216f, 0.145098f), new Color(0.1764706f, 0.1686275f, 0.1686275f), new Color(0.1764706f, 0.1686275f, 0.1686275f) };
        public Color blackStubble = new Color(0.3882353f, 0.2901961f, 0.2470588f);
        public Color[] elfHair = { new Color(0.9764706f, 0.9686275f, 0.9568628f), new Color(0.1764706f, 0.1686275f, 0.1686275f), new Color(0.8980393f, 0.7764707f, 0.6196079f) };
        public Color elfStubble = new Color(0.8627452f, 0.7294118f, 0.6862745f);

        [Header("Scar Colors")]
        public Color whiteScar = new Color(0.9294118f, 0.6862745f, 0.5921569f);
        public Color brownScar = new Color(0.6980392f, 0.5450981f, 0.4f);
        public Color blackScar = new Color(0.4235294f, 0.3176471f, 0.282353f);
        public Color elfScar = new Color(0.8745099f, 0.6588235f, 0.6313726f);

        [Header("Body Art Colors")]
        public Color[] bodyArt = { new Color(0.0509804f, 0.6745098f, 0.9843138f), new Color(0.7215686f, 0.2666667f, 0.2666667f), new Color(0.3058824f, 0.7215686f, 0.6862745f), new Color(0.9254903f, 0.882353f, 0.8509805f), new Color(0.3098039f, 0.7058824f, 0.3137255f), new Color(0.5294118f, 0.3098039f, 0.6470588f), new Color(0.8666667f, 0.7764707f, 0.254902f), new Color(0.2392157f, 0.4588236f, 0.8156863f) };

        // list of enabed objects on character
        [HideInInspector]
        public List<GameObject> enabledObjects = new List<GameObject>();
        //Finish to declare the variable for the color of the player


        //Declaring variable for the camera
        // reference to camera transform, used for rotation around the model during or after a randomization (this is sourced from Camera.main, so the main camera must be in the scene for this to work)
        Transform camHolder;

        // cam rotation x
        float x = 16;

        // cam rotation y
        float y = -30;

        private void Start()
        {
            //Get the SeedGeneratorScript
            seedGenerator = GetComponent<SeedGenerator>();
            //InitializeLists();
            male.Initialize();
            female.Initialize();
            allGender.Initialize();

            
            ShowLists(male, female, allGender);

            //Creation of the camera
            Transform cam = Camera.main.transform;
            if (cam)
            {
                cam.position = transform.position + new Vector3(0, 0.3f, 2);
                cam.rotation = Quaternion.Euler(0, -180, 0);
                camHolder = new GameObject().transform;
                camHolder.position = transform.position + new Vector3(0, 1, 0);
                cam.LookAt(camHolder);
                cam.SetParent(camHolder);
            }

        }

        
        public void GenerateWithSeed(int seedTemp)
        {
            

            if (enabledObjects.Count != 0)
            {
                foreach (GameObject g in enabledObjects)
                {
                    g.SetActive(false);
                }
            }
            enabledObjects.Clear();

            //Generate the basics
            seed = seedTemp;
            seedEnhancer = seedTemp;
            Gender gender = (Gender)(seed % 2);
            ClassChoose classOfPlayer = (ClassChoose)(seed % 5);
            //BuildLists(/*classOfPlayer*/);
            UpgraderOfSeedEnhancer((int)classOfPlayer);

            //Afficher dans l'inspecteur les charactéristique du personnage
            Debug.Log("La classe est " + (classOfPlayer.ToString()));
            Debug.Log("Le sex est " + (gender.ToString()));
            Debug.Log("La seed est : " + seed);

            BuildLists(classOfPlayer);
            
            //choosing the race
            Race race = (Race)(seedEnhancer % 2);
            UpgraderOfSeedEnhancer((int)race);

            //Choosing if we add element on the charcter
            Elements elements = (Elements)(seedEnhancer % 2);
            UpgraderOfSeedEnhancer((int)elements);

            //Choosing the type of cover if there is
            TypeHeadCovering headCovering = (TypeHeadCovering)(seedEnhancer % 3);
            UpgraderOfSeedEnhancer((int)headCovering);

            //choosing the color of the skin
            SkinColor skinColor = (race == Race.Human) ? (SkinColor)(seedEnhancer % 3) : SkinColor.Elf;
            UpgraderOfSeedEnhancer((int)skinColor);

            //Choosing the details if this is a male or female
            FacialHair facialHair = (gender == Gender.Male) ? (FacialHair)(seedEnhancer % 2) : FacialHair.No;
            UpgraderOfSeedEnhancer((int)facialHair);

            RandomizeByVariable(((gender == Gender.Male) ? male : female), allGender, gender, elements, race, facialHair, skinColor, headCovering, classOfPlayer);

            SexSelection pose = GetComponent<SexSelection>();
            switch (gender)
            {
                case Gender.Male:
                    pose.SetMan();
                    break;
                case Gender.Female:
                    pose.SetWoman();    
                    break;
            }

            seedGenerator.HasGenerate();

        }

        private void ModificateTheLists(CharacterObjectGroups maleModif, CharacterObjectGroups femaleModif, CharacterObjectListsAllGender allGenderModif, ClassChoose classOfPlayer)
        {
            ChangeSpecificList(maleModif.headAllElements, classOfPlayer);
            ChangeSpecificList(maleModif.headNoElements, classOfPlayer);
            ChangeSpecificList(maleModif.eyebrow, classOfPlayer);
            ChangeSpecificList(maleModif.facialHair, classOfPlayer);
            ChangeSpecificList(maleModif.torso, classOfPlayer);
            ChangeSpecificList(maleModif.arm_Upper_Right, classOfPlayer);
            ChangeSpecificList(maleModif.arm_Upper_Left, classOfPlayer);
            ChangeSpecificList(maleModif.arm_Lower_Right, classOfPlayer);
            ChangeSpecificList(maleModif.arm_Lower_Left, classOfPlayer);
            ChangeSpecificList(maleModif.hand_Right, classOfPlayer);
            ChangeSpecificList(maleModif.hand_Left, classOfPlayer);
            ChangeSpecificList(maleModif.hips, classOfPlayer);
            ChangeSpecificList(maleModif.leg_Right, classOfPlayer);
            ChangeSpecificList(maleModif.leg_Left, classOfPlayer);

            //build out femaleModif lists
            ChangeSpecificList(femaleModif.headAllElements, classOfPlayer);
            ChangeSpecificList(femaleModif.headNoElements, classOfPlayer);
            ChangeSpecificList(femaleModif.eyebrow, classOfPlayer);
            ChangeSpecificList(femaleModif.facialHair, classOfPlayer);
            ChangeSpecificList(femaleModif.torso, classOfPlayer);
            ChangeSpecificList(femaleModif.arm_Upper_Right, classOfPlayer);
            ChangeSpecificList(femaleModif.arm_Upper_Left, classOfPlayer);
            ChangeSpecificList(femaleModif.arm_Lower_Right, classOfPlayer);
            ChangeSpecificList(femaleModif.arm_Lower_Left, classOfPlayer);
            ChangeSpecificList(femaleModif.hand_Right, classOfPlayer);
            ChangeSpecificList(femaleModif.hand_Left, classOfPlayer);
            ChangeSpecificList(femaleModif.hips, classOfPlayer);
            ChangeSpecificList(femaleModif.leg_Right, classOfPlayer);
            ChangeSpecificList(femaleModif.leg_Left, classOfPlayer);

            // build out all gender lists
            ChangeSpecificList(allGenderModif.all_Hair, classOfPlayer);
            ChangeSpecificList(allGenderModif.all_Head_Attachment, classOfPlayer);
            ChangeSpecificList(allGenderModif.headCoverings_Base_Hair, classOfPlayer);
            ChangeSpecificList(allGenderModif.headCoverings_No_FacialHair, classOfPlayer);
            ChangeSpecificList(allGenderModif.headCoverings_No_Hair, classOfPlayer);
            ChangeSpecificList(allGenderModif.chest_Attachment, classOfPlayer);
            ChangeSpecificList(allGenderModif.back_Attachment, classOfPlayer);
            ChangeSpecificList(allGenderModif.shoulder_Attachment_Right, classOfPlayer);
            ChangeSpecificList(allGenderModif.shoulder_Attachment_Left, classOfPlayer);
            ChangeSpecificList(allGenderModif.elbow_Attachment_Right, classOfPlayer);
            ChangeSpecificList(allGenderModif.elbow_Attachment_Left, classOfPlayer);
            ChangeSpecificList(allGenderModif.hips_Attachment, classOfPlayer);
            ChangeSpecificList(allGenderModif.knee_Attachement_Right, classOfPlayer);
            ChangeSpecificList(allGenderModif.knee_Attachement_Left, classOfPlayer);
            ChangeSpecificList(allGenderModif.elf_Ear, classOfPlayer);
            ChangeSpecificList(allGenderModif.All_13_Hand_Attachement_Left, classOfPlayer);
            ChangeSpecificList(allGenderModif.All_13_Hand_Attachement_Right, classOfPlayer);
        }

        private void ShowLists(CharacterObjectGroups maleModif, CharacterObjectGroups femaleModif, CharacterObjectListsAllGender allGenderModif)
        {
            ShowList(maleModif.headAllElements);
            ShowList(maleModif.headNoElements);
            ShowList(maleModif.eyebrow);
            ShowList(maleModif.facialHair);
            ShowList(maleModif.torso);
            ShowList(maleModif.arm_Upper_Right);
            ShowList(maleModif.arm_Upper_Left);
            ShowList(maleModif.arm_Lower_Right);
            ShowList(maleModif.arm_Lower_Left);
            ShowList(maleModif.hand_Right);
            ShowList(maleModif.hand_Left);
            ShowList(maleModif.hips);
            ShowList(maleModif.leg_Right);
            ShowList(maleModif.leg_Left);

            //build out femaleModif lists
            ShowList(femaleModif.headAllElements);
            ShowList(femaleModif.headNoElements);
            ShowList(femaleModif.eyebrow);
            ShowList(femaleModif.facialHair);
            ShowList(femaleModif.torso);
            ShowList(femaleModif.arm_Upper_Right);
            ShowList(femaleModif.arm_Upper_Left);
            ShowList(femaleModif.arm_Lower_Right);
            ShowList(femaleModif.arm_Lower_Left);
            ShowList(femaleModif.hand_Right);
            ShowList(femaleModif.hand_Left);
            ShowList(femaleModif.hips);
            ShowList(femaleModif.leg_Right);
            ShowList(femaleModif.leg_Left);

            // build out all gender lists
            ShowList(allGenderModif.all_Hair);
            ShowList(allGenderModif.all_Head_Attachment);
            ShowList(allGenderModif.headCoverings_Base_Hair);
            ShowList(allGenderModif.headCoverings_No_FacialHair);
            ShowList(allGenderModif.headCoverings_No_Hair);
            ShowList(allGenderModif.chest_Attachment);
            ShowList(allGenderModif.back_Attachment);
            ShowList(allGenderModif.shoulder_Attachment_Right);
            ShowList(allGenderModif.shoulder_Attachment_Left);
            ShowList(allGenderModif.elbow_Attachment_Right);
            ShowList(allGenderModif.elbow_Attachment_Left);
            ShowList(allGenderModif.hips_Attachment);
            ShowList(allGenderModif.knee_Attachement_Right);
            ShowList(allGenderModif.knee_Attachement_Left);
            ShowList(allGenderModif.elf_Ear);
            ShowList(allGenderModif.All_13_Hand_Attachement_Left);
            ShowList(allGenderModif.All_13_Hand_Attachement_Right);
        }

        private void ShowList(List<GameObject> gameObjects)
        {
            Debug.LogWarning(gameObjects.ToString() + "has " + gameObjects.Count + " objects.");
        }
        //All the function and methods needed to randomize the 

        private void BuildLists(ClassChoose classChoose)
        {
            //build out male lists
            BuildList(male.headAllElements, "Male_Head_All_Elements", classChoose);
            BuildList(male.headNoElements, "Male_Head_No_Elements", classChoose);
            BuildList(male.eyebrow, "Male_01_Eyebrows", classChoose);
            BuildList(male.facialHair, "Male_02_FacialHair", classChoose);
            BuildList(male.torso, "Male_03_Torso", classChoose);
            BuildList(male.arm_Upper_Right, "Male_04_Arm_Upper_Right", classChoose);
            BuildList(male.arm_Upper_Left, "Male_05_Arm_Upper_Left", classChoose);
            BuildList(male.arm_Lower_Right, "Male_06_Arm_Lower_Right", classChoose);
            BuildList(male.arm_Lower_Left, "Male_07_Arm_Lower_Left", classChoose);
            BuildList(male.hand_Right, "Male_08_Hand_Right", classChoose);
            BuildList(male.hand_Left, "Male_09_Hand_Left", classChoose);
            BuildList(male.hips, "Male_10_Hips", classChoose);
            BuildList(male.leg_Right, "Male_11_Leg_Right", classChoose);
            BuildList(male.leg_Left, "Male_12_Leg_Left", classChoose);

            //build out female lists
            BuildList(female.headAllElements, "Female_Head_All_Elements", classChoose);
            BuildList(female.headNoElements, "Female_Head_No_Elements", classChoose);
            BuildList(female.eyebrow, "Female_01_Eyebrows", classChoose);
            BuildList(female.facialHair, "Female_02_FacialHair", classChoose);
            BuildList(female.torso, "Female_03_Torso", classChoose);
            BuildList(female.arm_Upper_Right, "Female_04_Arm_Upper_Right", classChoose);
            BuildList(female.arm_Upper_Left, "Female_05_Arm_Upper_Left", classChoose);
            BuildList(female.arm_Lower_Right, "Female_06_Arm_Lower_Right", classChoose);
            BuildList(female.arm_Lower_Left, "Female_07_Arm_Lower_Left", classChoose);
            BuildList(female.hand_Right, "Female_08_Hand_Right", classChoose);
            BuildList(female.hand_Left, "Female_09_Hand_Left", classChoose);
            BuildList(female.hips, "Female_10_Hips", classChoose);
            BuildList(female.leg_Right, "Female_11_Leg_Right", classChoose);
            BuildList(female.leg_Left, "Female_12_Leg_Left", classChoose);

            // build out all gender lists
            BuildList(allGender.all_Hair, "All_01_Hair", classChoose);
            BuildList(allGender.all_Head_Attachment, "All_02_Head_Attachment", classChoose);
            BuildList(allGender.headCoverings_Base_Hair, "HeadCoverings_Base_Hair", classChoose);
            BuildList(allGender.headCoverings_No_FacialHair, "HeadCoverings_No_FacialHair", classChoose);
            BuildList(allGender.headCoverings_No_Hair, "HeadCoverings_No_Hair", classChoose);
            BuildList(allGender.chest_Attachment, "All_03_Chest_Attachment", classChoose);
            BuildList(allGender.back_Attachment, "All_04_Back_Attachment", classChoose);
            BuildList(allGender.shoulder_Attachment_Right, "All_05_Shoulder_Attachment_Right", classChoose);
            BuildList(allGender.shoulder_Attachment_Left, "All_06_Shoulder_Attachment_Left", classChoose);
            BuildList(allGender.elbow_Attachment_Right, "All_07_Elbow_Attachment_Right", classChoose);
            BuildList(allGender.elbow_Attachment_Left, "All_08_Elbow_Attachment_Left", classChoose);
            BuildList(allGender.hips_Attachment, "All_09_Hips_Attachment", classChoose);
            BuildList(allGender.knee_Attachement_Right, "All_10_Knee_Attachement_Right", classChoose);
            BuildList(allGender.knee_Attachement_Left, "All_11_Knee_Attachement_Left", classChoose);
            BuildList(allGender.elf_Ear, "Elf_Ear", classChoose);
            BuildList(allGender.All_13_Hand_Attachement_Left, "All_13_Hand_Attachement_Right", classChoose);
            BuildList(allGender.All_13_Hand_Attachement_Right, "All_13_Hand_Attachement_Left", classChoose);

        }

        void BuildList(List<GameObject> targetList, string characterPart, ClassChoose classChoose)
        {
            Transform[] rootTransform = gameObject.GetComponentsInChildren<Transform>(true);

            // declare target root transform
            Transform targetRoot = null;

            // find character parts parent object in the scene
            foreach (Transform t in rootTransform)
            {
                if (t.gameObject.name == characterPart)
                {
                    targetRoot = t;
                    break;
                }
            }

            // clears targeted list of all objects
            targetList.Clear();

            // cycle through all child objects of the parent object
            for (int i = 0; i < targetRoot.childCount; i++)
            {
                // get child gameobject index i
                GameObject go = targetRoot.GetChild(i).gameObject;


                bool isCompatible = false;
                ClassHolder classHolder = go.GetComponent<ClassHolder>();
                if (classHolder == null)
                {
                    Debug.LogError(go.name + " do not have ClassHolderScript");

                }
                else
                {
                    isCompatible = classHolder.CheckClass(classChoose);
                }
                //add object to the targeted object list if it is compatible
                if (isCompatible)
                {
                    targetList.Add(go);
                }

                if (!mat)
                {
                    // collect the material for the random character, only if null in the inspector;
                    if (go.GetComponent<SkinnedMeshRenderer>())
                        mat = go.GetComponent<SkinnedMeshRenderer>().material;
                }
                
                // disable child object
                go.SetActive(false);



            }
        }

        // randomization method based on previously selected variables
        void RandomizeByVariable(CharacterObjectGroups cog, CharacterObjectListsAllGender allGenderModif, Gender gender, Elements elements, Race race, FacialHair facialHair, SkinColor skinColor, TypeHeadCovering headCovering, ClassChoose classChoose)
        {
            int index = 0;
            // if facial elements are enabled
            switch (elements)
            {
                case Elements.Yes:
                    //select head with all elements
                    if (cog.headAllElements.Count != 0)
                    {
                        index = IndexRandomizer(cog.headAllElements.Count);
                        ActivateItem(cog.headAllElements[index]);

                    }

                    //select eyebrows
                    if (cog.eyebrow.Count != 0)
                    {
                        index = IndexRandomizer(cog.eyebrow.Count);
                        ActivateItem(cog.eyebrow[index]);
                    }


                    //select facial hair (conditional)
                    if (cog.facialHair.Count != 0 && facialHair == FacialHair.Yes && gender == Gender.Male && headCovering != TypeHeadCovering.HeadCoverings_No_FacialHair)
                    {
                        index = IndexRandomizer(cog.facialHair.Count);
                        ActivateItem(cog.facialHair[index]);
                    }

                    // select hair attachment
                    switch (headCovering)
                    {
                        case TypeHeadCovering.HeadCoverings_Base_Hair:
                            // set hair attachment to index 1
                            if (allGenderModif.all_Hair.Count != 0)
                                ActivateItem(allGenderModif.all_Hair[1]);
                            if (allGenderModif.headCoverings_Base_Hair.Count != 0)
                            {
                                index = IndexRandomizer(allGenderModif.headCoverings_Base_Hair.Count);
                                ActivateItem(allGenderModif.headCoverings_Base_Hair[index]);
                            }
                            break;
                        case TypeHeadCovering.HeadCoverings_No_FacialHair:
                            // no facial hair attachment
                            if (allGenderModif.all_Hair.Count != 0)
                            {
                                index = IndexRandomizer(allGenderModif.all_Hair.Count);
                                ActivateItem(allGenderModif.all_Hair[index]);
                            }
                            if (allGenderModif.headCoverings_No_FacialHair.Count != 0)
                            {
                                index = IndexRandomizer(allGenderModif.headCoverings_No_FacialHair.Count);
                                ActivateItem(allGenderModif.headCoverings_No_FacialHair[index]);

                            }
                            break;
                        case TypeHeadCovering.HeadCoverings_No_Hair:
                            // select hair attachment
                            if (allGenderModif.headCoverings_No_Hair.Count != 0)
                            {
                                index = IndexRandomizer(allGenderModif.all_Hair.Count);
                                ActivateItem(allGenderModif.all_Hair[index]);
                            }
                            // if not human
                            if (race != Race.Human)
                            {
                                // select elf ear attachment
                                if (allGenderModif.elf_Ear.Count != 0)
                                {
                                    index = IndexRandomizer(allGenderModif.elf_Ear.Count);
                                    ActivateItem(allGenderModif.elf_Ear[index]);
                                }
                            }
                            break;
                    }
                    break;

                case Elements.No:
                    //select head with no elements
                    if (cog.headNoElements.Count != 0)
                    {
                        index = seedEnhancer % cog.headNoElements.Count;
                        ActivateItem(cog.headNoElements[index]);
                        UpgraderOfSeedEnhancer(index);
                    }
                    break;
            }

            // select torso starting at index 1
            if (cog.torso.Count != 0)
            {
                index = Mathf.Max(1, seedEnhancer % cog.torso.Count);
                ActivateItem(cog.torso[index]);
                UpgraderOfSeedEnhancer(index);

            }

            // determine chance for upper arms to be different and activate
            if (cog.arm_Upper_Right.Count != 0)
                RandomizeLeftRight(cog.arm_Upper_Right, cog.arm_Upper_Left, 15);

            // determine chance for lower arms to be different and activate
            if (cog.arm_Lower_Right.Count != 0)
                RandomizeLeftRight(cog.arm_Lower_Right, cog.arm_Lower_Left, 15);

            // determine chance for hands to be different and activate
            if (cog.hand_Right.Count != 0)
                RandomizeLeftRight(cog.hand_Right, cog.hand_Left, 15);

            // select hips starting at index 1
            if (cog.hips.Count != 0)
            {
                index = Mathf.Max(1, seedEnhancer % cog.hips.Count);
                UpgraderOfSeedEnhancer(index);
                ActivateItem(cog.hips[index]);

            }

            // determine chance for legs to be different and activate
            if (cog.leg_Right.Count != 0)
                RandomizeLeftRight(cog.leg_Right, cog.leg_Left, 15);

            // select chest attachment
            if (allGenderModif.chest_Attachment.Count != 0)
            {
                index = IndexRandomizer(allGenderModif.chest_Attachment.Count);
                ActivateItem(allGenderModif.chest_Attachment[index]);

            }

            // select back attachment
            if (allGenderModif.back_Attachment.Count != 0)
            {
                index = IndexRandomizer(allGenderModif.back_Attachment.Count);
                ActivateItem(allGenderModif.back_Attachment[index]);

            }

            // determine chance for shoulder attachments to be different and activate
            if (allGenderModif.shoulder_Attachment_Right.Count != 0)
                RandomizeLeftRight(allGenderModif.shoulder_Attachment_Right, allGenderModif.shoulder_Attachment_Left, 10);

            // determine chance for elbow attachments to be different and activate
            if (allGenderModif.elbow_Attachment_Right.Count != 0)
                RandomizeLeftRight(allGenderModif.elbow_Attachment_Right, allGenderModif.elbow_Attachment_Left, 10);

            // select hip attachment
            if (allGenderModif.hips_Attachment.Count != 0)
            {
                index = IndexRandomizer(allGenderModif.hips_Attachment.Count);
                ActivateItem(allGenderModif.hips_Attachment[index]);
            }

            // determine chance for knee attachments to be different and activate
            if (allGenderModif.knee_Attachement_Right.Count != 0)
                RandomizeLeftRight(allGenderModif.knee_Attachement_Right, allGenderModif.knee_Attachement_Left, 10);


            // start randomization of the random characters colors
            RandomizeColors(skinColor);
        }

        private void UpgraderOfSeedEnhancer(int upgrader)
        {
            seedEnhancer += (upgrader + 1) * (seedEnhancer % 3);
        }

        // method for handling the chance of left/right items to be differnt (such as shoulders, hands, legs, arms)
        void RandomizeLeftRight(List<GameObject> objectListRight, List<GameObject> objectListLeft, int rndPercent)
        {
            // rndPercent = chance for left item to be different

            int index = IndexRandomizer(objectListRight.Count);
            ActivateItem(objectListRight[index]);
            // search if the second arms is going to be different
            if (GetPercent(rndPercent))
            {
                index = IndexRandomizer(objectListLeft.Count);
            }

            // enable left item from list using index
            ActivateItem(objectListLeft[index]);
        }

        // enable game object and add it to the enabled objects list
        void ActivateItem(GameObject go)
        {
            // enable item
            go.SetActive(true);

            // add item to the enabled items list
            enabledObjects.Add(go);
        }

        bool GetPercent(int pct)
        {
            bool p = false;
            int roll = seedEnhancer%100;
            if (roll <= pct)
            {
                p = true;
            }
            UpgraderOfSeedEnhancer(roll);
            return p;
        }

        void RandomizeColors(SkinColor skinColor)
        {
            // set skin and hair colors based on skin color roll
            switch (skinColor)
            {
                case SkinColor.White:
                    // randomize and set white skin, hair, stubble, and scar color
                    RandomizeAndSetHairSkinColors("White", whiteSkin, whiteHair, whiteStubble, whiteScar);
                    break;

                case SkinColor.Brown:
                    // randomize and set brown skin, hair, stubble, and scar color
                    RandomizeAndSetHairSkinColors("Brown", brownSkin, brownHair, brownStubble, brownScar);
                    break;

                case SkinColor.Black:
                    // randomize and black elf skin, hair, stubble, and scar color
                    RandomizeAndSetHairSkinColors("Black", blackSkin, blackHair, blackStubble, blackScar);
                    break;

                case SkinColor.Elf:
                    // randomize and set elf skin, hair, stubble, and scar color
                    RandomizeAndSetHairSkinColors("Elf", elfSkin, elfHair, elfStubble, elfScar);
                    break;
            }
            int index = 0;
            // randomize and set primary color
            if (primary.Length != 0)
            {
                index = IndexRandomizer(primary.Length);
                mat.SetColor("_Color_Primary", primary[index]);
            }
            else
                Debug.Log("No Primary Colors Specified In The Inspector");

            // randomize and set secondary color
            if (secondary.Length != 0)
            {
                index = IndexRandomizer(secondary.Length);
                mat.SetColor("_Color_Secondary", secondary[index]);
            }
            else
                Debug.Log("No Secondary Colors Specified In The Inspector");

            // randomize and set primary metal color
            if (metalPrimary.Length != 0)
            {
                index = IndexRandomizer(metalPrimary.Length);
                mat.SetColor("_Color_Metal_Primary", metalPrimary[index]);

            }
            else
                Debug.Log("No Primary Metal Colors Specified In The Inspector");

            // randomize and set secondary metal color
            if (metalSecondary.Length != 0)
            {
                index = IndexRandomizer(metalSecondary.Length);
                mat.SetColor("_Color_Metal_Secondary", metalSecondary[index]);
            }
            else
                Debug.Log("No Secondary Metal Colors Specified In The Inspector");

            // randomize and set primary leather color
            if (leatherPrimary.Length != 0)
            {
                index = IndexRandomizer(leatherPrimary.Length);
                mat.SetColor("_Color_Leather_Primary", leatherPrimary[index]);

            }
            else
                Debug.Log("No Primary Leather Colors Specified In The Inspector");

            // randomize and set secondary leather color
            if (leatherSecondary.Length != 0)
            {
                index = IndexRandomizer(leatherSecondary.Length);
                mat.SetColor("_Color_Leather_Secondary", leatherSecondary[index]);
            }
                
            else
                Debug.Log("No Secondary Leather Colors Specified In The Inspector");

            // randomize and set body art color
            if (bodyArt.Length != 0)
            {
                index = IndexRandomizer(bodyArt.Length);
                mat.SetColor("_Color_BodyArt", bodyArt[index]);

            }
            else
                Debug.Log("No Body Art Colors Specified In The Inspector");

            // randomize and set body art amount

            int pourcentage = seedEnhancer % 100;
            UpgraderOfSeedEnhancer(pourcentage);
            mat.SetFloat("_BodyArt_Amount", pourcentage/100);
        }

        void RandomizeAndSetHairSkinColors(string info, Color[] skin, Color[] hair, Color stubble, Color scar)
        {
            int index = 0;
            // randomize and set elf skin color
            if (skin.Length != 0)
            {
                index = IndexRandomizer(skin.Length);
                mat.SetColor("_Color_Skin", skin[index]);
            }
            else
            {
                Debug.Log("No " + info + " Skin Colors Specified In The Inspector");
            }

            // randomize and set elf hair color
            if (hair.Length != 0)
            {
                index = IndexRandomizer(hair.Length);
                mat.SetColor("_Color_Hair", hair[index]);
            }
            else
            {
                Debug.Log("No " + info + " Hair Colors Specified In The Inspector");
            }

            // set stubble color
            mat.SetColor("_Color_Stubble", stubble);

            // set scar color
            mat.SetColor("_Color_Scar", scar);
        }

        int IndexRandomizer(int lenghtOfList)
        {
            int newIndex = seedEnhancer % lenghtOfList;
            UpgraderOfSeedEnhancer(newIndex);
            return newIndex;

        }

        public void ChangeSpecificList(List<GameObject> listeToChange, ClassChoose classChoose)
        {
            List<GameObject> returnList = new List<GameObject>();
            for (int i = 0; i < listeToChange.Count; ++i)
            {
                ClassHolder classHolder = listeToChange[i].GetComponent<ClassHolder>();
                if (classHolder != null)
                {
                    if (classHolder.CheckClass(classChoose))
                    {
                        returnList.Add(listeToChange[i]);
                    }
                }
                else
                {
                    Debug.LogError(listeToChange[i].name + " do not have a ClassHolder script");
                }
                
            }
            listeToChange.Clear();
            listeToChange = returnList;
        }

        //Ici je vais gérer la caméra
        private void Update()
        {
            if (camHolder)
            {
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    x += 1 * Input.GetAxis("Mouse X");
                    y -= 1 * Input.GetAxis("Mouse Y");
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
                else
                {
                    x -= 1 * Input.GetAxis("Horizontal");
                    y -= 1 * Input.GetAxis("Vertical");
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
        private void LateUpdate()
        {
            // method for handling the camera rotation around the character
            if (camHolder)
            {
                y = Mathf.Clamp(y, -45, 15);
                camHolder.eulerAngles = new Vector3(y, x, 0.0f);
            }
        }
    }
    //-------------Fin de la précédente classe, c'est un rappelle.



    [System.Serializable]
    public class CharacterObjectGroups
    {
        public List<GameObject> headAllElements;
        public List<GameObject> headNoElements;
        public List<GameObject> eyebrow;
        public List<GameObject> facialHair;
        public List<GameObject> torso;
        public List<GameObject> arm_Upper_Right;
        public List<GameObject> arm_Upper_Left;
        public List<GameObject> arm_Lower_Right;
        public List<GameObject> arm_Lower_Left;
        public List<GameObject> hand_Right;
        public List<GameObject> hand_Left;
        public List<GameObject> hips;
        public List<GameObject> leg_Right;
        public List<GameObject> leg_Left;

        public void Initialize()
        {
            headAllElements = new List<GameObject>();
            headNoElements = new List<GameObject>();
            eyebrow = new List<GameObject>();
            facialHair = new List<GameObject>();
            torso = new List<GameObject>();
            arm_Upper_Right = new List<GameObject>();
            arm_Upper_Left = new List<GameObject>();
            arm_Lower_Right = new List<GameObject>();
            arm_Lower_Left = new List<GameObject>();
            hand_Right = new List<GameObject>();
            hand_Left = new List<GameObject>();
            hips = new List<GameObject>();
            leg_Right = new List<GameObject>();
            leg_Left = new List<GameObject>();
        }
    }

    // classe for keeping the lists organized, allows for organization of the all gender items
    [System.Serializable]
    public class CharacterObjectListsAllGender
    {
        public List<GameObject> headCoverings_Base_Hair;
        public List<GameObject> headCoverings_No_FacialHair;
        public List<GameObject> headCoverings_No_Hair;
        public List<GameObject> all_Hair;
        public List<GameObject> all_Head_Attachment;
        public List<GameObject> chest_Attachment;
        public List<GameObject> back_Attachment;
        public List<GameObject> shoulder_Attachment_Right;
        public List<GameObject> shoulder_Attachment_Left;
        public List<GameObject> elbow_Attachment_Right;
        public List<GameObject> elbow_Attachment_Left;
        public List<GameObject> hips_Attachment;
        public List<GameObject> knee_Attachement_Right;
        public List<GameObject> knee_Attachement_Left;
        //public List<GameObject> all_12_Extra;
        public List<GameObject> elf_Ear;
        public List<GameObject> All_13_Hand_Attachement_Right;
        public List<GameObject> All_13_Hand_Attachement_Left;

        public void Initialize()
        {
            headCoverings_Base_Hair = new List<GameObject>();
            headCoverings_No_FacialHair = new List<GameObject>();
            headCoverings_No_Hair = new List<GameObject>();
            all_Hair = new List<GameObject>();
            all_Head_Attachment = new List<GameObject>();
            chest_Attachment = new List<GameObject>();
            back_Attachment = new List<GameObject>();
            shoulder_Attachment_Right = new List<GameObject>();
            shoulder_Attachment_Left = new List<GameObject>();
            elbow_Attachment_Right = new List<GameObject>();
            elbow_Attachment_Left = new List<GameObject>();
            hips_Attachment = new List<GameObject>();
            knee_Attachement_Right = new List<GameObject>();
            knee_Attachement_Left = new List<GameObject>();
            //all_12_Extra = new List<GameObject>();
            elf_Ear = new List<GameObject>();
            All_13_Hand_Attachement_Right = new List<GameObject>();
            All_13_Hand_Attachement_Left = new List<GameObject>();

        }
        
        
    }


}
