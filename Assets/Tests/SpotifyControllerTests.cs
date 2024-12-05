using NUnit.Framework;
using UnityEngine;
using System.Threading.Tasks;
using System;

using static UnityEngine.EventSystems.EventTrigger;

public class SpotifyControllerTests
{
    private GameObject spotifyControllerSingleton;

    [SetUp]
    public async Task SetUpAsync()
    {
        spotifyControllerSingleton = new GameObject("SpotifyControllerSingleton");
        var spotifyController = spotifyControllerSingleton.AddComponent<SpotifyController>();
        await spotifyController.Init();
    }

    [Test]
    public async Task SpotifyController_PausesAsync()
    {
        await spotifyControllerSingleton.GetComponent<SpotifyController>().Pause();

        await Task.Delay(3000);

        var isPaused = await spotifyControllerSingleton.GetComponent<SpotifyController>().GetPlayPauseState();
        Assert.IsFalse(isPaused, "Expected the playback to be paused.");
    }

    [Test]
    public async Task SpotifyController_PlaysAsync()
    {
        await spotifyControllerSingleton.GetComponent<SpotifyController>().Play();

        await Task.Delay(3000);

        var isPaused = await spotifyControllerSingleton.GetComponent<SpotifyController>().GetPlayPauseState();
        Assert.IsTrue(isPaused, "Expected the playback to be playing.");
    }

    [Test]
    public async Task SpotifyController_TogglePausePlayAsync()
    {
        await spotifyControllerSingleton.GetComponent<SpotifyController>().TogglePlayPause();
        await Task.Delay(2000);

        bool state = await spotifyControllerSingleton.GetComponent<SpotifyController>().GetPlayPauseState();
        await Task.Delay(2000);

        await spotifyControllerSingleton.GetComponent<SpotifyController>().TogglePlayPause();
        await Task.Delay(2000);

        bool newState = await spotifyControllerSingleton.GetComponent<SpotifyController>().GetPlayPauseState();

        Assert.AreNotEqual(state, newState);
    }

    [Test]
    public async Task SpotifyController_FastForwardAsync()
    {
        var startTime = 0;

        await spotifyControllerSingleton.GetComponent<SpotifyController>().Pause();
        startTime = await spotifyControllerSingleton.GetComponent<SpotifyController>().GetCurrentSongProgressMillis();

        var duration = 10;
        await spotifyControllerSingleton.GetComponent<SpotifyController>().FastForward(duration);

        await Task.Delay(2000);

        var difference = await spotifyControllerSingleton.GetComponent<SpotifyController>().GetCurrentSongProgressMillis() - startTime;
        Assert.IsTrue(Math.Abs(duration - (difference / 1000)) <= 1);
    }

    [Test]
    public async Task SpotifyController_RewindAsync()
    {
        var startTime = 0;

        await spotifyControllerSingleton.GetComponent<SpotifyController>().Pause();
        startTime = await spotifyControllerSingleton.GetComponent<SpotifyController>().GetCurrentSongProgressMillis();

        var duration = 10;
        await spotifyControllerSingleton.GetComponent<SpotifyController>().Rewind(duration);

        await Task.Delay(2000);

        var difference = await spotifyControllerSingleton.GetComponent<SpotifyController>().GetCurrentSongProgressMillis() - startTime;
        Assert.IsTrue(Math.Abs(duration - (difference / 1000)) >= 1);
    }

    [Test]
    public async Task SpotifyController_NextAsync()
    {
        await spotifyControllerSingleton.GetComponent<SpotifyController>().Pause();
        await Task.Delay(2000);
        await spotifyControllerSingleton.GetComponent<SpotifyController>().Next();

        await Task.Delay(2000);
        var isPaused = await spotifyControllerSingleton.GetComponent<SpotifyController>().GetPlayPauseState();

        Assert.IsTrue(isPaused, "Expected the playback to be played.");
    }

    [Test]
    public async Task SpotifyController_PreviousAsync()
    {
        await spotifyControllerSingleton.GetComponent<SpotifyController>().Pause();
        await Task.Delay(2000);
        await spotifyControllerSingleton.GetComponent<SpotifyController>().Previous();
        await Task.Delay(2000);

        var isPaused = await spotifyControllerSingleton.GetComponent<SpotifyController>().GetPlayPauseState();
        Assert.IsTrue(isPaused, "Expected the playback to be played.");

    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(spotifyControllerSingleton);
    }
}
