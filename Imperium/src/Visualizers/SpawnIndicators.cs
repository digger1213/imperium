#region

using Imperium.MonoBehaviours.VisualizerObjects;
using Imperium.Oracle;
using Imperium.Util;
using Imperium.Util.Binding;
using UnityEngine;

#endregion

namespace Imperium.Visualizers;

internal class SpawnIndicators(
    ImpBinding<OracleState> oracleStateBinding,
    ImpBinding<bool> visibleBinding
) : BaseVisualizer<OracleState>(oracleStateBinding, visibleBinding)
{
    protected override void Refresh(OracleState state)
    {
        ClearObjects();

        for (var i = state.currentCycle; i < state.outdoorCycles.Length; i++)
        {
            foreach (var spawnReport in state.outdoorCycles[i])
            {
                var indicatorObject = Object.Instantiate(ImpAssets.SpawnIndicator);
                var indicator = indicatorObject.AddComponent<SpawnIndicator>();
                indicator.transform.position = spawnReport.position;
                indicator.Init(
                    Imperium.ObjectManager.GetDisplayName(spawnReport.entity.enemyName),
                    spawnReport.spawnTime
                );

                indicatorObjects[indicatorObject.GetInstanceID()] = indicatorObject;
            }
        }
    }
}