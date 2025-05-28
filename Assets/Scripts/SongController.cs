using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SongController : MonoBehaviour
{
    // Get A Folder With The Song Files //
    // Get The Album Cover - image file //
    // Get Song Data - .chart file //
    // Load In Notes - object //
    // Spawn Notes In After X Ticks //

    public string songFolder = "C:\\Users\\rapha\\My project\\Assets\\Soundtracks\\feelGoodInc";

    public Image albumCoverGUI;

    public TextMeshProUGUI songName;
    public TextMeshProUGUI artist;
    public TextMeshProUGUI charter;
    public TextMeshProUGUI year;

    public List<Note> notes;

    // Sample values for BPM and resolution
    public float currentBPM = 120f;  // Beats per minute
    public int resolution = 192;       // Ticks per beat

    public float bpmStartTime = 0;     // Time when the current BPM started
    public float bpmStartTick = 0;     // Tick when the current BPM started

    public float songStartTime = 0f;

    public GameObject noteObj;
    public Material[] noteMaterials;
    public Transform[] notePos;

    public float distanceFromDrum = 4f;

    AudioSource audioSource;

    public float delay = 5f;

    public float getTravelTime()
    {
        return noteObj.transform.position.x / noteObj.GetComponent<NoteController>().speed;
    }

    public void Start()
    {
        notes = new List<Note>();
        audioSource = GetComponent<AudioSource>();
        ////Debug.Log($"Audio Source: {audioSource}");


        // Chart File - Get Song Data //
        LoadChartData();

        // Chart File - Get Note Data //
        LoadNotes();

        // Start Song //
        StartCoroutine(ClipPlay());
    }

    IEnumerator PlaySong()
    {

        songStartTime = Time.time - 9.5f;  // Record the start time of the song
        ////Debug.Log($"Song Start Time: {songStartTime}");


        foreach (var note in notes)
        {
            float noteTime = TicksToSeconds(note.tick);  // Convert tick to time
            float waitTime = noteTime - (Time.time - songStartTime);  // Calculate how long to wait
            ////Debug.Log($"Note Time: {noteTime}, Wait Time: {waitTime}");

            if (waitTime > 0)
            {
                yield return new WaitForSeconds(waitTime);  // Wait for the right time to spawn the note
            }

            SpawnNote(note);  // Spawn the note
        }
        GameManager.endGame();
    }


    IEnumerator ClipPlay()
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
        StartCoroutine(PlaySong());
    }


    void SpawnNote(Note note)
    {
        // Implement the logic to spawn a note in the game
        // ////Debug.Log($"Spawning note {note.noteType} at tick {note.tick}");

        GameObject newNote;
        int t = 0;

        switch (note.noteType)
        {
            case Note.NoteType.green:
                t = 0;
                break;
            case Note.NoteType.red:
                t = 1;
                break;
            case Note.NoteType.yellow:
                t = 2;
                break;
            case Note.NoteType.blue:
                t = 3;
                break;
            case Note.NoteType.orange:
                t = 4;
                break;
        }
        ////Debug.Log($"Spawning note {note.noteType} at tick {note.tick} - {note.noteDurration}");
        newNote = Instantiate(noteObj, notePos[t].position, notePos[t].rotation);
        newNote.GetComponent<Renderer>().material = noteMaterials[t];
    }



    void LoadChartData()
    {
        ////Debug.Log($"Loading chart data from: {songFolder}");

        string chartPath = Path.Combine(songFolder, "notes.chart");
        if (File.Exists(chartPath))
        {
            ////Debug.Log($"Chart file found at: {chartPath}");

            string[] lines = File.ReadAllLines(chartPath);

            ////Debug.Log($"Lines: {lines.Length}");

            bool isInSongSection = false;
            /*
            foreach (var line in lines)
            {
                //Debug.Log($"Line: {line}");
                if (line.Trim() == "[Song]")
                {
                    isInSongSection = true;
                    //Debug.Log("Song section found");
                }
                else if (line.Trim().StartsWith("[") && isInSongSection)
                {
                    //Debug.Log("End of song section found");
                    // We've reached the end of the song metadata section
                    break;
                }

                if (isInSongSection)
                {
                    if (line.StartsWith("  Name ="))
                        songName.text = line.Split('=')[1].Trim().Trim('"');
                    else if (line.StartsWith("  Artist ="))
                        artist.text = line.Split('=')[1].Trim().Trim('"');
                    else if (line.StartsWith("  Charter ="))
                        charter.text = line.Split('=')[1].Trim().Trim('"');
                    else if (line.StartsWith("  Year ="))
                        year.text = line.Split('=')[1].Trim().Trim('"').Trim(',').Trim(' ');
                }
            }
            //Debug.Log($"Song Name: {songName.text}");
            //Debug.Log($"Artist: {artist.text}");
            //Debug.Log($"Charter: {charter.text}");
            //Debug.Log($"Year: {year.text}");
            */

            // Song delay timer //

            ////Debug.Log($"Loading delay from: {songFolder}");
            lines = File.ReadAllLines(chartPath);
            isInSongSection = false;
            foreach (var line in lines)
            {
                if (line.Trim() == "[SyncTrack]")
                {

                    isInSongSection = true;
                    ////Debug.Log("SyncTrack found");
                }
                else if (line.Trim().StartsWith("[") && isInSongSection)
                {
                    // We've reached the end of the song metadata section
                    break;
                }

                if (isInSongSection)
                {
                    if (line.StartsWith("  0 = TS "))
                    {
                        delay = line.Split("  0 = TS ")[1].Trim(' ')[0] / 14f;
                    }
                        
                }
            }

            // Song bpm

            lines = File.ReadAllLines(chartPath);
            isInSongSection = false;
            foreach (var line in lines)
            {
                if (line.Trim() == "[SyncTrack]")
                {
                    isInSongSection = true;
                }
                else if (line.Trim().StartsWith("[") && isInSongSection)
                {
                    // We've reached the end of the song metadata section
                    break;
                }

                if (isInSongSection)
                {
                    if (line.StartsWith("  0 = B "))
                    {
                        //Debug.Log($"BPM found: {line}");
                        string _ = line.Split("  0 = B ")[1].Trim(' ').Substring(0, 3);
                        currentBPM = float.Parse(_);

                        //Debug.Log($"Current BPM: {currentBPM}");
                    }

                }
            }
        }
        else
        {
            //debug.LogError($"Chart file not found at: {chartPath}");
        }
    }

    void LoadNotes()
    {
        ////Debug.Log($"Loading notes from: {songFolder}");
        string chartPath = Path.Combine(songFolder, "notes.chart");
        if (File.Exists(chartPath))
        {
            string[] lines = File.ReadAllLines(chartPath);
            bool isNoteSection = false;
            notes = new List<Note>();

            foreach (var line in lines)
            {
                if (line.Trim() == "[EasyDrums]")
                {
                    //Debug.Log("ExpertDrums section found");
                    isNoteSection = true;
                    continue;
                }

                if (isNoteSection)
                {
                    if (line.Trim().StartsWith("["))
                        break; // End of note section

                    // Process the note line
                    if (line.Contains(" = N "))
                    {
                        string[] parts = line.Split(new[] { " = N " }, System.StringSplitOptions.RemoveEmptyEntries);
                        float tick = float.Parse(parts[0].Trim());
                        string[] noteParts = parts[1].Split(' ');
                        int noteKey = int.Parse(noteParts[0]);
                        float duration = float.Parse(noteParts[1]);

                        Note note = new Note
                        {
                            tick = tick,
                            noteType = Note.GetNoteType(noteKey),
                            noteDurration = duration
                        };
                        notes.Add(note);

                    }
                }
            }
        }
        else
        {
            Debug.LogError($"Chart file not found at: {chartPath}");
        }
    }

    // Converts ticks to seconds based on the current BPM and resolution
    public float TicksToSeconds(float ticks)
    {
        float secondsPerBeat = 60 / currentBPM;
        float deltaTicks = ticks - bpmStartTick;
        float deltaBeats = deltaTicks / resolution;
        float deltaSeconds = deltaBeats * secondsPerBeat;
        return deltaSeconds + bpmStartTime;
    }

    // Converts seconds to ticks based on the current BPM and resolution
    public float SecondsToTicks(float seconds)
    {
        float secondsPerBeat = 60 / currentBPM;
        float deltaSeconds = seconds - bpmStartTime;
        float deltaBeats = deltaSeconds / secondsPerBeat;
        float deltaTicks = deltaBeats * resolution;
        return deltaTicks + bpmStartTick;
    }
}

public struct Note
{
    public enum NoteType
    {
        green,  // Assume 0
        red,    // Assume 1
        yellow, // Assume 2
        blue,   // Assume 3
        orange  // Assume 4
    }

    public float tick;
    public NoteType noteType;
    public float noteDurration;

    public static NoteType GetNoteType(int key)
    {
        switch (key)
        {
            case 0: return NoteType.green;
            case 1: return NoteType.red;
            case 2: return NoteType.yellow;
            case 3: return NoteType.blue;
            case 4: return NoteType.orange;
            case 5: return NoteType.orange;
            case 6: return NoteType.orange;
            default: return NoteType.green; // Default case if note key is unrecognized
        }
    }

    
}