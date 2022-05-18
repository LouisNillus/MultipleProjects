using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//CE SRIPT DOIT ÊTRE PLACÉ DANS UN DOSSIER "Editor" : Assets/Editor par exemple.

[CustomEditor(typeof(PaintingTool))]
public class CustomInspectorPaintingTool : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PaintingTool script = (PaintingTool)target;
        EditorGUILayout.BeginVertical();

        if (GUILayout.Button("Save Custom Room")) //Bouton pour sauvegarder une Custom Room.
        {
            CustomRoom cr = new CustomRoom();
            cr.dimensions = script.myCustomRoom.dimensions;
            cr.roomName = script.myCustomRoom.roomName;
            cr.draw = script.myCustomRoom.draw;
            script.savedRooms.Add(cr);
        }

        if (GUILayout.Button("Clear All")) //Bouton pour effacer les Custom Rooms sauvegardées.
        {
            script.savedRooms.Clear();
        }

        var centeredStyle = GUI.skin.GetStyle("HelpBox");
        centeredStyle.alignment = TextAnchor.UpperCenter;
        GUILayout.Label("Thank you to Brecht Lecluyse who made the ConditionalHideAttribute (www.brechtos.com)", centeredStyle); //Cadre de crédits pour le ConditionalHideAttribute.

        EditorGUILayout.EndVertical();
    }

    void OnSceneGUI()
    {
        PaintingTool script = (PaintingTool)target;
        Event e = Event.current;
        switch (e.type)
        {
            case EventType.KeyDown:
                if (Event.current.keyCode == KeyCode.Keypad6) //Touche DROITE du selector
                {
                    script.columnSelector += script.selectorSpeed;
                }
                if (Event.current.keyCode == KeyCode.Keypad4) //Touche GAUCHE du selector
                {
                    script.columnSelector -= script.selectorSpeed;
                }
                if (Event.current.keyCode == KeyCode.Keypad8) //Touche HAUT du selector
                {
                    script.rowSelector += script.selectorSpeed;
                }
                if (Event.current.keyCode == KeyCode.Keypad5) //Touche BAS du selector
                {
                    script.rowSelector -= script.selectorSpeed;
                }
                if(Event.current.keyCode == KeyCode.KeypadPlus && script.drawSelector == true) //Touche d'alternance entre le mode Paint et le mode Erase
                {
                    if (script.brushType == BrushType.Paint)
                        script.brushType = BrushType.Erase;
                    else if (script.brushType == BrushType.Erase)
                        script.brushType = BrushType.Paint;
                }

                script.rowSelector = Mathf.Clamp(script.rowSelector, 0, 100);
                script.columnSelector = Mathf.Clamp(script.columnSelector, 0, 100);

                if (Event.current.keyCode == KeyCode.Space && script.drawSelector == true) //Touche pour Paint ou Erase (en fonction du mode choisi)
                {
                    script.SetTile((int)script.selectedTile.x, (int)script.selectedTile.y);
                }
                break;
        }
    }
}
