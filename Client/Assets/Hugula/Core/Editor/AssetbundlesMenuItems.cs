﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;
using System.Linq;
using Hugula;
using Hugula.Loader;
using Hugula.Editor;

public class AssetbundlesMenuItems
{

    #region unity5 AssetBundles export

    //[MenuItem("Assets/AssetBundles/Build AssetBundles", false, 2)]
    [MenuItem("AssetBundles/Build AssetBundles &b", false, 1)]
    static public void BuildAssetBundles()
    {
        BuildScript.BuildAssetBundles();
    }

    [MenuItem("AssetBundles/Generate/Update All AssetBundle Name", false, 2)]
    static public void UpdateAssetBundlesName()
    {
        var allAssets = AssetDatabase.GetAllAssetPaths().Where(path =>
            (path.StartsWith("Assets/CustomerResource")
            || path.StartsWith("Assets/TapEnjoy"))
            && !(path.EndsWith(".cs"))
            ).ToArray();

        BuildScript.UpdateAssetBundlesName(allAssets);
    }

    [MenuItem("AssetBundles/Generate/AssetBundle Md5Mapping ", false, 5)]
    static public void GenerateAssetBundlesMd5Mapping()
    {
        var allAssets = AssetDatabase.GetAllAssetPaths().Where(path =>
            (path.StartsWith("Assets/CustomerResource")
				|| path.StartsWith("Assets/Scene"))
            && !(path.EndsWith(".cs"))
        ).ToArray();
        BuildScript.GenerateAssetBundlesMd5Mapping(allAssets);
    }

    [MenuItem("AssetBundles/Generate/AssetBundle Update File ", false, 6)]
    static public void GenerateAssetBundlesUpdate()
    {
        ExportResources.buildAssetBundlesUpdateAB();
    }

    [MenuItem("AssetBundles/PlayerPrefs DeleteAll ", false, 7)]
    static public void PlayerPrefsDeleteAll()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs.DeleteAll()");
    }

    // [MenuItem("AssetBundles/--------", false, 11)]
    // static void Breaker_AssetBundles() { }


    [MenuItem("Assets/AssetBundles/Set AssetBundle Name", false, 1)]
    static public void SetAssetBundlesName()
    {
        BuildScript.SetAssetBundlesName(true);
    }

    [MenuItem("Assets/AssetBundles/Set AssetBundle Variants And Name【parent folder name is varint name】", false, 2)]
    static public void SetAssetBundlesVariantsAndName()
    {
        BuildScript.SetAssetBundlesVariantsAndName();
    }

    [MenuItem("Assets/HugulaExtends/Set Extends Floder", false, 3)]
    static public void SetExtendsFloder()
    {
        BuildScript.SetAsExtendsFloder();
    }

    [MenuItem("Assets/HugulaExtends/Add Extension Files", false, 3)]
    static public void AddExtensionFiles()
    {
        BuildScript.ExtensionFiles(false);
    }

       [MenuItem("Assets/HugulaExtends/Remove Extension Files", false, 3)]
    static public void RemoveExtensionFiles()
    {
        BuildScript.ExtensionFiles(true);
    }

   [MenuItem("Assets/HugulaExtends/Add Zip Files", false, 3)]
    static public void AddZipFiles()
    {
        BuildScript.ZipFiles(false);
    }

       [MenuItem("Assets/HugulaExtends/Remove Zip Files", false, 3)]
    static public void RemoveZipFiles()
    {
        BuildScript.ZipFiles(true);
    }

    [MenuItem("Assets/HugulaExtends/Clear Extends Floder", false, 5)]
    static public void ClearExtendsFloder()
    {
        BuildScript.ClearExtendsFloder();
    }

    [MenuItem("Assets/AssetBundles/Clear AssetBundle Name", false, 7)]
    static public void ClearAssetBundlesName()
    {
        BuildScript.ClearAssetBundlesName();
    }

    [MenuItem("Assets/AssetBundles/Clear UnUsed AssetBundle Name", false, 8)]
    static public void ClearUnUsedAssetBundlesName()
    {
        BuildScript.ClearUnUsedAssetBundlesName();
    }

    [MenuItem("Assets/AssetBundles/Update Selected AssetBundle Name", false, 10)]
    static public void UpdateSelectedAssetBundleNames()
    {
        Object[] selection = Selection.objects;
        List<string> allAssetPaths = new List<string>();
        foreach (Object s in selection)
        {
			string filepath = AssetDatabase.GetAssetPath (s);
			if (!File.Exists (filepath)) {
				string dirpath = filepath.Replace (Application.dataPath, "");
				Debug.Log (dirpath);
				var allAssets = AssetDatabase.GetAllAssetPaths().Where(path =>
					(path.StartsWith(dirpath+"/") || path.StartsWith(dirpath+"\\"))
					&& !(path.EndsWith(".cs"))
				).ToArray();

				allAssetPaths.AddRange (allAssets);
			}else
				allAssetPaths.Add(filepath);
        }

//		foreach (var s in allAssetPaths)
//			Debug.Log (s);
        BuildScript.UpdateAssetBundlesName(allAssetPaths.ToArray());
    }

     [MenuItem("Assets/AssetBundles/Delete Selected Prefab's AssetBundle ", false, 11)]
    static public void DeleteAssetBundlesName()
    {
        BuildScript.DeleteAssetBundlesName();
    }

    const string kSimulationMode = "AssetBundles/Simulation Mode";

    [MenuItem(kSimulationMode)]
    public static void ToggleSimulationMode()
    {
        CResLoader.SimulateAssetBundleInEditor = !CResLoader.SimulateAssetBundleInEditor;
    }

    [MenuItem(kSimulationMode, true)]
    public static bool ToggleSimulationModeValidate()
    {
        Menu.SetChecked(kSimulationMode, CResLoader.SimulateAssetBundleInEditor);
        return true;
    }

    #endregion

    #region lua language config export
    // [MenuItem("Hugula/-------- ", false, 11)]
    // static void Breaker() { }
    [MenuItem("Assets/Export Selected Lua ", false, 12)]
    public static void exportSelectedLua()
    {
        Object[] selection = Selection.objects;
        List<string> allAssetPaths = new List<string>();
        foreach (Object s in selection)
        {
			string filepath = AssetDatabase.GetAssetPath (s);
			if (!File.Exists (filepath)) {
				string dirpath = filepath.Replace (Application.dataPath, "");
				Debug.Log (dirpath);
				var allAssets = AssetDatabase.GetAllAssetPaths().Where(path =>
					(path.StartsWith(dirpath+"/") || path.StartsWith(dirpath+"\\"))
					&& (path.EndsWith(".lua"))
				).ToArray();
				allAssetPaths.AddRange (allAssets);
			}else
				allAssetPaths.Add(filepath);
        }

        foreach(var p in allAssetPaths){ Debug.Log(p);}  

        ExportResources.doExportLua(allAssetPaths.ToArray());
    }

    [MenuItem("Assets/Change To BytesAsset ", false, 13)]
    public static void changeSelectedToBytesAsset()
    {
        Object[] selection = Selection.objects;
        List<string> allAssetPaths = new List<string>();
        foreach (Object s in selection)
        {
			string filepath = AssetDatabase.GetAssetPath (s);
			if (!File.Exists (filepath)) {
				string dirpath = filepath.Replace (Application.dataPath, "");
				Debug.Log (dirpath);
				var allAssets = AssetDatabase.GetAllAssetPaths().Where(path =>
					(path.StartsWith(dirpath+"/") || path.StartsWith(dirpath+"\\"))
					&& (path.EndsWith(".bytes") || path.EndsWith(".txt"))
				).ToArray();
				allAssetPaths.AddRange (allAssets);
			}else
				allAssetPaths.Add(filepath);
        }

        foreach(var p in allAssetPaths){ Debug.Log(p);}  

        BuildScript.ChangeAssetsToBytesAsset(allAssetPaths.ToArray());
    }
    

    [MenuItem("Hugula/Export Lua [Assets\\Lua *.lua] %l", false, 12)]
    public static void exportLua()
    {
        ExportResources.exportLua();
    }

    [MenuItem("Hugula/Export Config [Assets\\Config *.csv]", false, 13)]
    public static void exportConfig()
    {
        ExportResources.exportConfig();
    }

    [MenuItem("Hugula/Export Language [Assets\\Config\\Lan *.csv]", false, 14)]
    public static void exportLanguage()
    {
        ExportResources.exportLanguage();
    }

    [MenuItem("Hugula/Build For Publish ", false, 16)]
    public static void exportPublish()
    {
        ExportResources.exportPublish();
    }

    #endregion

    #region hugula debug
    const string kDebugLuaAssetBundlesMenu = "Hugula/Debug Lua";

    [MenuItem(kDebugLuaAssetBundlesMenu, false, 1)]
    public static void ToggleSimulateAssetBundle()
    {
        PLua.isDebug = !PLua.isDebug;
    }

    [MenuItem(kDebugLuaAssetBundlesMenu, true, 1)]
    public static bool ToggleSimulateAssetBundleValidate()
    {
        Menu.SetChecked(kDebugLuaAssetBundlesMenu, PLua.isDebug);
        return true;
    }
    #endregion

    #region 加密
    //[MenuItem("Hugula/AES/", false, 10)]
    //static void Breaker2() { }

    // [MenuItem("Hugula/AES/GenerateKey", false, 12)]
    // static void GenerateKey()
    // {
    //     ExportResources.GenerateKey();
    // }

    // [MenuItem("Hugula/AES/GenerateIV", false, 13)]
    // static void GenerateIV()
    // {
    //     ExportResources.GenerateIV();
    // }

    #endregion
}
