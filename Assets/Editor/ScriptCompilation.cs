
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

// Adds a hotkey to (re)compile scripts.
// Useful when Unity doesn't recompile scripts automatically.
// (This can happen when using external code editors like neovim)
public static class ScriptCompilationHotkey
{
   [MenuItem("Compile/Force Script Compilation #r")] // #r adds shift+r as the hotkey
   private static void ForceRecompile()
   {
      Debug.Log("Forcing script compilation...");
      CompilationPipeline.RequestScriptCompilation();
   }
}

