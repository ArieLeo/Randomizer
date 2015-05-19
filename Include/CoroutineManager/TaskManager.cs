// Copyright (c) 2011, Ken Rockot  <k-e-n-@-REMOVE-CAPS-AND-HYPHENS-oz.gs>.  All rights reserved.
// Added a inline delegate to it. @onevcat
// Everyone is granted non-exclusive license to do anything at all with this code.

using System.Collections;
using UnityEngine;

namespace RandomizerEx {

    internal class TaskManager : MonoBehaviour {

        public class TaskState {

            public bool Running {
                get { return running; }
            }

            public bool Paused {
                get { return paused; }
            }

            public delegate void FinishedHandler(bool manual);

            public event FinishedHandler Finished;

            private IEnumerator coroutine;
            private bool running;
            private bool paused;
            private bool stopped;

            public TaskState(IEnumerator c) {
                coroutine = c;
            }

            public void Pause() {
                paused = true;
            }

            public void Unpause() {
                paused = false;
            }

            public void Start() {
                running = true;
                singleton.StartCoroutine(CallWrapper());
            }

            public void Stop() {
                stopped = true;
                running = false;
            }

            private IEnumerator CallWrapper() {
                yield return null;
                IEnumerator e = coroutine;
                while (running) {
                    if (paused)
                        yield return null;
                    else {
                        if (e != null && e.MoveNext()) {
                            yield return e.Current;
                        }
                        else {
                            running = false;
                        }
                    }
                }

                FinishedHandler handler = Finished;
                if (handler != null)
                    handler(stopped);
            }

        }

        private static TaskManager singleton;

        public static TaskState CreateTask(IEnumerator coroutine) {
            if (singleton == null) {
                GameObject go = new GameObject("TaskManager");
                singleton = go.AddComponent<TaskManager>();
            }
            return new TaskState(coroutine);
        }

    }

}
