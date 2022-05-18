using BaseX;
using FrooxEngine;
using FrooxEngine.UIX;
using HarmonyLib;
using NeosModLoader;
using System;

namespace InspectorExpandAll
{
    public class InspectorExpandAll : NeosMod
    {
        public override string Name => "InspectorExpandAll";
        public override string Author => "badhaloninja";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/badhaloninja/InspectorExpandAll";

        //static Uri expandAll = new Uri("neosdb:///0d31852049f7a88e3e607c883d47b18f8a37fb5071a6e8b4db2507c23206c9e7.png");
        static Uri expandAllBold = new Uri("neosdb:///aa0dc4316f0d94d5c1d0681b43788f3109398b250bd736e4b6612a735427fb0d.png");

        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("me.badhaloninja.InspectorExpandAll");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(SceneInspector), "OnAttach")]
        private class SceneInspector_OnAttach_Patch
        {
            public static void Postfix(SyncRef<Slot> ____hierarchyContentRoot, SyncRef<Sync<string>> ____rootText)
            {
                var buttonsRoot = ____rootText.Target.Slot.Parent;

                var ui = new UIBuilder(buttonsRoot);

                ui.Style.FlexibleWidth = -1f;
                ui.Style.MinWidth = 64f;
                var button = ui.Button(expandAllBold);

                button.LocalPressed += (btn, evnt) =>
                {
                    ____hierarchyContentRoot.Target.ForeachComponentInChildren<Expander>(exp =>
                    {
                        exp.IsExpanded = true;
                    });
                };
                button.Slot.InsertAtIndex(1);
            }
        }
    }
}