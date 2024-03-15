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
        [SerializeField] private Texture2D UnitTargetLockedEnemyCursorTexture;
        [SerializeField] private Texture2D UnitTargetUnlockedAllyCursorTexture;
        [SerializeField] private Texture2D UnitTargetLockedAllyCursorTexture;
        [SerializeField] private Texture2D FlagCursorTexture;
        [SerializeField] private Texture2D DiscoverCursorTexture;
        [SerializeField] private Texture2D InteractCursorTexture;

        public static MouseCursorManager Instance;

        private Vector2 cursorOffset;

        private MouseCursorMode[] cursorTargetingModes = new[]
        {
            MouseCursorMode.PointTargetCrossHair,
            MouseCursorMode.UnitTargetUnlockedEnemy,
            MouseCursorMode.UnitTargetLockedEnemy,
            MouseCursorMode.UnitTargetUnlockedAlly,
            MouseCursorMode.UnitTargetLockedAlly
        };

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

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
                case MouseCursorMode.Attack:
                    nextCursor = AttackCursorTexture;
                    break;
                case MouseCursorMode.PointTargetCrossHair:
                    nextCursor = CastAtEnemyCursorTexture;
                    break;
                case MouseCursorMode.UnitTargetUnlockedEnemy:
                    nextCursor = CastAtAllyCursorTexture;
                    break;
                case MouseCursorMode.UnitTargetLockedEnemy:
                    nextCursor = UnitTargetLockedEnemyCursorTexture;
                    break;
                case MouseCursorMode.UnitTargetUnlockedAlly:
                    nextCursor = UnitTargetUnlockedAllyCursorTexture;
                    break;
                case MouseCursorMode.UnitTargetLockedAlly:
                    nextCursor = UnitTargetLockedAllyCursorTexture;
                    break;
                case MouseCursorMode.Flag:
                    nextCursor = FlagCursorTexture;
                    break;
                case MouseCursorMode.Discover:
                    nextCursor = DiscoverCursorTexture;
                    break;
                case MouseCursorMode.Interact:
                    nextCursor = InteractCursorTexture;
                    break;
                default:
                    return false;
            }

            if (IsTargetingMode(mode))
            {
                cursorOffset.x = nextCursor.width / 2f;
                cursorOffset.y = nextCursor.height / 2f;
            }
            else
            {
                cursorOffset = Vector2.zero;
            }

            Cursor.SetCursor(nextCursor, cursorOffset, CursorMode.Auto);
            
            return true;
        }

        private bool IsTargetingMode(MouseCursorMode mode) => cursorTargetingModes.Contains(mode);
    }

    public enum MouseCursorMode
    {
        // Default cursors
        Regular,
        RegularHighlightGreen,
        RegularHighlightRed,
        
        // Ability targeting cursors
        PointTargetCrossHair,
        UnitTargetUnlockedEnemy,
        UnitTargetLockedEnemy,
        UnitTargetUnlockedAlly,
        UnitTargetLockedAlly,
        
        // Control cursors
        Flag,
        Discover,
        Interact,
        Attack
        
    }

}