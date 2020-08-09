using System;
using BepInEx;

namespace SkipIntroLogos
{
	using UnityEngine;

	[BepInPlugin("org.bepinex.plugins.humanfallflat.skipintrologos", "Skip Intro Logos", "1.0.0.0")]
	[BepInProcess("Human.exe")]
	class DeleteStartup : BaseUnityPlugin
	{
		private void Start()
		{
			base.StartCoroutine(this.SkipStartupExperience());
		}

		private System.Collections.IEnumerator SkipStartupExperience()
		{
			// use built-in "SkipStartupExperience" intended for accepting invites, but pass "null" so that it goes straight to menu!
			while (!StartupExperienceController.instance || !MenuCameraEffects.instance)
			{
				yield return null;
			}
			yield return null;
			yield return null;
			yield return null;
			StartupExperienceController.instance.SkipStartupExperience(null);

			// for aesthetic
			MenuCameraEffects.instance.BlackOut();
			StartupExperienceController.instance.FadeOutDim();

			// get rid of intro music
			while (!IntroDrones.instance)
			{
				yield return null;
			}
			IntroDrones ins = IntroDrones.instance;
			ins.sound.Pause();
			ins.sound.Stop();
			IntroDrones.instance = null;
			UnityEngine.Object.Destroy(ins.gameObject);
		}
	}
}
