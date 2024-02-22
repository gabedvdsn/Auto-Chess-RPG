using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AutoChessRPG
{
    public class MouseCursorManager : MonoBehaviour
    {
        [SerializeField] private Texture2D RegularCursorTexture;
        [SerializeField] private Texture2D RegularHighlightGreenCursorTexture;
        [SerializeField] private Texture2D RegularHighlightRedCursorTexture;
        [SerializeField] private Texture2D CastAtEnemyCursorTexture;
        [SerializeField] private Texture2D CastAtAllyCursorTexture;
        [SerializeField] private Texture2D AttackCursorTexture;

        private Vector2 cursorOffset;

        private MouseCursorMode[] cursorCrossHairModes = new[]
        {
            MouseCursorMode.CastAtEnemyCrossHair,
            MouseCursorMode.CastAtAllyCrossHair
        };

        private void Start()
        {
            cursorOffset = Vector2.zero;
            
            SetCursorMode(MouseCursorMode.Regular);
        }

        public bool SetCursorMode(MouseCursorMode mode) => ChangeCursor(mode);

        private bool ChangeCursor(MouseCursorMode mode)
        {
            Texture2D nextCursor;
                
            switch (mode)
            {
                case MouseCursorMode.Regular:
                    nextCursor = RegularCursorTexture;
                    break;
                case MouseCursorMode.RegularHighlightGreen:
                    nextCursor = RegularHighlightGreenCursorTexture;
                    break;
                case MouseCursorMode.RegularHighlightRed:
                    nextCursor = RegularHighlightRedCursorTexture;
                    break;
                case MouseCursorMode.CastAtEnemyCrossHair:
                    nextCursor = CastAtEnemyCursorTexture;
                    break;
                case MouseCursorMode.CastAtAllyCrossHair:
                    nextCursor = CastAtAllyCursorTexture;
                    break;
                case MouseCursorMode.Attack:
                    nextCursor = AttackCursorTexture;
                    break;
                default:
                    return false;
            }

            if (CursorModeIsCrossHair(mode))
            {
                cursorOffset.x = nextCursor.width / 2;
                cursorOffset.y = nextCursor.height / 2;
            }
            else
            {
                cursorOffset = Vector2.zero;
            }

            Cursor.SetCursor(nextCursor, cursorOffset, CursorMode.Auto);
            
            return true;
        }

        private bool CursorModeIsCrossHair(MouseCursorMode mode) => cursorCrossHairModes.Contains(mode);
    }

    public enum MouseCursorMode
    {
        Regular,
        RegularHighlightGreen,
        RegularHighlightRed,
        CastAtEnemyCrossHair,
        CastAtAllyCrossHair,
        Attack
    }

}