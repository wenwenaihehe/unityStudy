#if UNITY_2019_3_OR_NEWER
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.TestTools;

namespace UnityEditor.AddressableAssets.Tests
{
    public class DomainReloadTests
    {
#if UNITY_2020_2_OR_NEWER
        bool savedState;
        EnterPlayModeOptions savedOptions;

        [UnitySetUp]
        public IEnumerator RuntimeSetup()
        {
            savedState = EditorSettings.enterPlayModeOptionsEnabled;
            EditorSettings.enterPlayModeOptionsEnabled = true;
            savedOptions = EditorSettings.enterPlayModeOptions;
            EditorSettings.enterPlayModeOptions = EnterPlayModeOptions.DisableDomainReload;
            Addressables.reinitializeAddressables = true;
            
            Assert.False(Application.isPlaying);
            yield return new EnterPlayMode(false);
        }
        
        [UnityTearDown]
        public IEnumerator RuntimeTearDown()
        {
            yield return new ExitPlayMode();
            EditorSettings.enterPlayModeOptionsEnabled = savedState;
            EditorSettings.enterPlayModeOptions = savedOptions;
#if !UNITY_EDITOR
            Assert.IsTrue(Addressables.reinitializeAddressables);
#endif
            Assert.False(Application.isPlaying);
        }
#endif
        
        [Test]
        [Ignore("https://jira.unity3d.com/browse/ADDR-2233")]
        public void DomainReloadTests_ReInitAddressablesFlagIsSetCorrectly_WhenExitingPlaymode()
        {
#if !UNITY_2020_2_OR_NEWER
            Assert.Ignore($"Skipping Domain Reload test {nameof(DomainReloadTests_ReInitAddressablesFlagIsSetCorrectly_WhenExitingPlaymode)}, Domain Reload tests supported from 2020.2+");
#else
            Assert.True(Application.isPlaying);
            Addressables.ResolveInternalId("DummyString"); //just need this so m_Addressables property gets called
            Assert.IsFalse(Addressables.reinitializeAddressables);
#endif
        }
    }
}
#endif
