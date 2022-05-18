using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//Thank you to Brecht Lecluyse who made the ConditionalHideAttribute (www.brechtos.com)

public enum Templates { Room_3x3, Room_5x5, Room_7x7, Room_9x9, Room_10x10, Room_5x10, Room_10x5 }
public enum LineType { Row, Column }
public enum BrushType { Paint, Erase }
public enum PixelResolution { _4x4, _8x8, _16x16, _32x32, _64x64, _128x128, _256x256, _512x512, _1024x1024 }

[ExecuteInEditMode]
    public class PaintingTool : MonoBehaviour
    {
        public bool hideGizmos;

        [Header("Colors")]
        public bool showColors;
        [ConditionalHide("showColors", true)]
        public Color32 zeroPointColor;
        [ConditionalHide("showColors", true)]
        public Color32 axisColor;
        [ConditionalHide("showColors", true)]
        public Color32 columnsColor;
        [ConditionalHide("showColors", true)]
        public Color32 rowsColor;
        [ConditionalHide("showColors", true)]
        public Color32 templatesColor;
        [ConditionalHide("showColors", true)]
        public Color32 rulersColor;
        [ConditionalHide("showColors", true)]
        public Color32 selectorColor;
        [ConditionalHide("showColors", true)]
        public Color32 customRoomsColor;


        [Header("Resolution Settings")]
        [ConditionalHide("customResolution", false, true)]
        public bool automaticPixelResolution = true;
        [ConditionalHide("automaticPixelResolution", ConditionalSourceField2 = "customResolution", InverseCondition1 = true, InverseCondition2 = true)]
        public PixelResolution pixelResolution;
        [ConditionalHide("automaticPixelResolution", false, true)]
        public bool customResolution;
        [ConditionalHide("customResolution", true)]
        public int myResolution;
        private int pixelEnumIndex;
        private int pixelsResolution;
        private float pix;
        private Grid grid;

        [Header("Drawing Settings")]
        public bool drawRowsAndColumns;
        [ConditionalHide("drawRowsAndColumns", true)]
        public Vector2 roomSize;
        [Space(5)]
        [ConditionalHide("drawRowsAndColumns", true, false, 0, 100)]
        public int howManyRows = 25;
        [ConditionalHide("drawRowsAndColumns", true, false, 0, 10)]
        public int rowsStep = 2;
        [Space(5)]
        [ConditionalHide("drawRowsAndColumns", true, false, 0, 100)]
        public int howManyColumns = 25;
        [ConditionalHide("drawRowsAndColumns", true, false, 0, 10)]
        public int columnsStep = 2;

        [Header("Templates")]
        public bool drawTemplates;
        [ConditionalHide("drawTemplates", true)]
        public bool overrideAxis;
        [ConditionalHide("drawTemplates", true)]
        public List<Templates> templatesToDraw = new List<Templates>();

        [Header("Rulers")]
        public bool drawRulers;
        [ConditionalHide("drawRulers", true, false, 0, 100)]
        public int rowRuler;
        [ConditionalHide("drawRulers", true, false, 0, 100)]
        public int columnRuler;

        [Header("Selector")]
        public bool drawSelector;
        [ConditionalHide("drawSelector", true, false, 1, 10)]
        public int selectorSpeed = 1;
        [ConditionalHide("drawSelector", true)]
        public Vector2 selectedTile;
        [ConditionalHide("drawSelector", true, false, 0, 100)]
        public int rowSelector;
        [ConditionalHide("drawSelector", true, false, 0, 100)]
        public int columnSelector;

        [Header("Painting")]
        public BrushType brushType;
        public bool paintOnMovement;
        public Tilemap tilemap;
        public Tile asset;

        [Header("Custom Rooms")]
        public bool drawCustomRooms;
        public CustomRoom myCustomRoom;
        public List<CustomRoom> savedRooms = new List<CustomRoom>();

        // Use this for initialization
        void Awake()
        {
            DefaultColors();
        }

        void OnDrawGizmos()
        {
#if UNITY_EDITOR

            if (Application.isPlaying == true)
                return;

            if (hideGizmos == false)
            {
                NoLagAttribution();

                //Draw X and Y axis
                Gizmos.color = axisColor;
                Gizmos.DrawLine(Vector3.zero, new Vector3(0, int.MaxValue, 0));
                Gizmos.DrawLine(Vector3.zero, new Vector3(int.MaxValue, 0, 0));

                //Draw Lines
                Gizmos.color = rowsColor;
                if (rowsStep > 0 && drawRowsAndColumns == true)
                {
                    for (int i = rowsStep; i <= howManyRows * rowsStep; i += rowsStep)
                    {
                        LineDrawer(LineType.Row, int.MaxValue, i);
                    }
                }

                //Draw Raws
                Gizmos.color = columnsColor;
                if (columnsStep > 0 && drawRowsAndColumns == true)
                {
                    for (int i = columnsStep; i <= howManyColumns * columnsStep; i += columnsStep)
                    {
                        LineDrawer(LineType.Column, i, int.MaxValue);
                    }
                }

                //Draw Template
                Gizmos.color = templatesColor;
                if (drawTemplates == true && templatesToDraw.Count > 0)
                {
                    foreach (Templates t in templatesToDraw)
                    {
                        switch (t)
                        {
                            case Templates.Room_3x3:
                                RoomDrawer(3, 3);
                                break;
                            case Templates.Room_5x5:
                                RoomDrawer(5, 5);
                                break;
                            case Templates.Room_7x7:
                                RoomDrawer(7, 7);
                                break;
                            case Templates.Room_9x9:
                                RoomDrawer(9, 9);
                                break;
                            case Templates.Room_10x10:
                                RoomDrawer(10, 10);
                                break;
                            case Templates.Room_5x10:
                                RoomDrawer(5, 10);
                                break;
                            case Templates.Room_10x5:
                                RoomDrawer(10, 5);
                                break;
                        }
                    }
                }

                //Draw Rulers
                Gizmos.color = rulersColor;
                if (drawRulers == true)
                {
                    if (rowRuler > 0)
                    {
                        LineDrawer(LineType.Row, int.MaxValue, rowRuler);
                    }

                    if (columnRuler > 0)
                    {
                        LineDrawer(LineType.Column, columnRuler, int.MaxValue);
                    }
                }

                //Draw Selector
                Gizmos.color = selectorColor;
                if (drawSelector == true)
                {
                    if (rowSelector > 0)
                    {
                        RoomDrawer(int.MaxValue, rowSelector, true);
                    }
                    if (columnSelector > 0)
                    {
                        RoomDrawer(columnSelector, int.MaxValue, true);
                    }
                }

                //Draw Custom Rooms
                Gizmos.color = customRoomsColor;
                if(drawCustomRooms == true)
                {
                    foreach (CustomRoom cr in savedRooms)
                    {
                        if (cr.draw == true)
                        {
                            RoomDrawer((int)cr.dimensions.x, (int)cr.dimensions.y);
                        }
                    }
                }

                //Draw 0,0 point
                Gizmos.color = zeroPointColor;
                Gizmos.DrawCube(Vector3.zero, new Vector3(0.1f, 0.1f, 1f));
            }
#endif
        }

        public void NoLagAttribution()
        {
            if (grid == null)
                grid = FindObjectOfType<Grid>();

            if (pixelEnumIndex != (int)pixelResolution)
                PixelResolutionFromEnum();

            if (customResolution == true && pixelsResolution != myResolution && myResolution > 0)
            {
                pixelsResolution = myResolution;
                grid.cellSize = new Vector3(pixelsResolution/100f, pixelsResolution/100f, 0);
                pixelEnumIndex = 999;
            }
            else if(customResolution == true && pixelsResolution != myResolution && myResolution <= 0)
            {
                Debug.LogError("Resolution can't be less or equal to 0");
            }

            if (pix != pixelsResolution / 100f)
                pix = pixelsResolution / 100f;

            if (roomSize != new Vector2(columnsStep, rowsStep))
                roomSize = new Vector2(columnsStep, rowsStep);

            if (selectedTile != new Vector2(columnSelector, rowSelector))
                selectedTile = new Vector2(columnSelector, rowSelector);

            if (paintOnMovement == true && drawSelector == true)
                SetTile((int)selectedTile.x, (int)selectedTile.y);
        }

        public void RoomDrawer(int x, int y, bool selectorMode = false)
        {
            float finalX = x * pix;
            float finalY = y * pix;
            Gizmos.DrawLine(new Vector3(finalX, 0, 0), new Vector3(finalX, finalY, 0));
            Gizmos.DrawLine(new Vector3(0, finalY, 0), new Vector3(finalX, finalY, 0));
            if (overrideAxis == true && selectorMode == false)
            {
                Gizmos.DrawLine(new Vector3(finalX, 0, 0), Vector3.zero);
                Gizmos.DrawLine(new Vector3(0, finalY, 0), Vector3.zero);
            }
            if (selectorMode == true)
            {
                Gizmos.DrawLine(new Vector3(finalX, 0, 0), new Vector3(finalX - pix, 0, 0));
                Gizmos.DrawLine(new Vector3(0, finalY, 0), new Vector3(0, finalY - pix, 0));
                Gizmos.DrawLine(new Vector3(finalX - pix, 0, 0), new Vector3(finalX, finalY, 0));
                Gizmos.DrawLine(new Vector3(0, finalY - pix, 0), new Vector3(finalX, finalY, 0));
            }
        }

        public void LineDrawer(LineType lineType, int x, int y)
        {
            float finalX = x * pix;
            float finalY = y * pix;
            if (lineType == LineType.Row)
            {
                Gizmos.DrawLine(new Vector3(0, finalY, 0), new Vector3(finalX, finalY, 0));
            }
            else if (lineType == LineType.Column)
            {
                Gizmos.DrawLine(new Vector3(finalX, 0, 0), new Vector3(finalX, finalY, 0));
            }
        }

        public void SetTile(int x, int y)
        {
            switch (brushType)
            {
                case BrushType.Paint:
                    tilemap.SetTile(new Vector3Int(x - 1, y - 1, 0), asset);
                    break;
                case BrushType.Erase:
                    tilemap.SetTile(new Vector3Int(x - 1, y - 1, 0), null);
                    break;
            }

        }

        public void PixelResolutionFromEnum()
        {
            switch (pixelResolution)
            {
                case PixelResolution._4x4:
                    pixelsResolution = 4;
                    pixelEnumIndex = 0;
                    break;
                case PixelResolution._8x8:
                    pixelsResolution = 8;
                    pixelEnumIndex = 1;
                    break;
                case PixelResolution._16x16:
                    pixelsResolution = 16;
                    pixelEnumIndex = 2;
                    break;
                case PixelResolution._32x32:
                    pixelsResolution = 32;
                    pixelEnumIndex = 3;
                    break;
                case PixelResolution._64x64:
                    pixelsResolution = 64;
                    pixelEnumIndex = 4;
                    break;
                case PixelResolution._128x128:
                    pixelsResolution = 128;
                    pixelEnumIndex = 5;
                    break;
                case PixelResolution._256x256:
                    pixelsResolution = 256;
                    pixelEnumIndex = 6;
                    break;
                case PixelResolution._512x512:
                    pixelsResolution = 512;
                    pixelEnumIndex = 7;
                    break;
                case PixelResolution._1024x1024:
                    pixelsResolution = 1024;
                    pixelEnumIndex = 8;
                    break;
            }
            grid.cellSize = new Vector3(pixelsResolution / 100f, pixelsResolution / 100f, 0);
        }

        public void DefaultColors()
        {
            zeroPointColor = Color.red;
            axisColor = Color.red;
            columnsColor = Color.white;
            rowsColor = Color.white;
            templatesColor = Color.blue;
            rulersColor = Color.green;
            selectorColor = Color.yellow;
            customRoomsColor = Color.cyan;
        }
    }

    [System.Serializable]
    public class CustomRoom
    {
        public string roomName;
        public Vector2Int dimensions;
        public bool draw = true;
    }
