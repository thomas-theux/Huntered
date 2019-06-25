﻿using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;


    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }


    private void Start() {
        Play("Theme");
    }


    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning("Sound " + name + " couldn't be found!");
            return;
        }

        s.source.Play();
    }


    public void PlayOneShot(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning("Sound " + name + " couldn't be found!");
            return;
        }

        s.source.PlayOneShot(s.clip);
    }


    public void PlayRandom(string name, float pMin, float pMax) {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning("Sound " + name + " couldn't be found!");
            return;
        }

        // Add random pitch
        float rndPitch = UnityEngine.Random.Range(pMin, pMax);
        s.source.pitch = rndPitch;

        s.source.Play();
    }


    public void Stop(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) {
            Debug.LogWarning("Sound " + name + " couldn't be found!");
            return;
        }

        s.source.Stop();
    }

}
