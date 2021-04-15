﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    static Queue<ICommand> commandBuffer;

    static List<ICommand> commandHistory;
    static int counter;

    private void Awake()
    {
        commandBuffer = new Queue<ICommand>();
        commandHistory = new List<ICommand>();
    }

    public static void AddCommand(ICommand command)
    {
        while (commandHistory.Count > counter)
        {
            commandHistory.RemoveAt(counter);
        }

        commandBuffer.Enqueue(command);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Command history length: " + commandHistory.Count);
        
        if(commandBuffer.Count > 0)
        {
            ICommand c = commandBuffer.Dequeue();
            c.Execute();

            commandHistory.Add(c);
            counter++;    
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                if(counter > 0)
                {
                    counter--;
                    commandHistory[counter].Undo();
                }
            }
            else if(Input.GetKeyDown(KeyCode.R))
            {
                if(counter < commandHistory.Count)
                {
                    commandHistory[counter].Execute();
                    counter++;
                }
            }
        }
    }
}
