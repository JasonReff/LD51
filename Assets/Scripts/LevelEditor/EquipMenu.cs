using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

namespace LevelEditor
{
    public class EquipMenu : LevelEditorEquippable
    {
        [SerializeField] private Image _image;
        [SerializeField] private LevelEditorManager _manager;
        [SerializeField] private LevelEditorMenu _menu;
        [SerializeField] private List<EquipTool> _tools;
        private LevelEditorTool _equippedTool;
        [SerializeField] private EquipTool _toolUIPrefab;
        [SerializeField] private float _toolDistance = 5f, _toolMovementDuration = 1f;

        private void OnEnable()
        {
            OnToolEquipped += AnyToolEquipped;
        }

        private void OnDisable()
        {
            OnToolEquipped -= AnyToolEquipped;
        }

        public void EquipOrShow()
        {
            if (_equippedTool == null && !MenuExists())
            {
                ShowMenu();
                return;
            }
            if (MenuExists())
            {
                HideMenu();
                return;
            }
            if (_equippedTool != null && (_manager.EquippedTool == null || _manager.EquippedTool != _equippedTool))
            {
                Equip();
                return;
            }
            if (_manager.EquippedTool != null && _manager.EquippedTool == _equippedTool)
            {
                ShowMenu();
            }
        }

        public void Equip()
        {
            OnToolEquipped?.Invoke(_equippedTool);
            
        }

        public void ShowMenu()
        {
            for (int i = 0; i < _menu.Tools.Count; i++)
            {
                var tool = Instantiate(_toolUIPrefab, transform.position, _toolUIPrefab.transform.rotation, transform);
                tool.Initialize(_menu.Tools[i]);
                _tools.Add(tool);
                MoveToolUI(tool, i);
            }
        }

        private void MoveToolUI(EquipTool toolUI, int index)
        {
            toolUI.transform.SetSiblingIndex(index);
            toolUI.transform.DOMoveY(transform.position.y + (index + 1) * _toolDistance, _toolMovementDuration);
        }

        private IEnumerator HideToolUI(EquipTool toolUI)
        {
            toolUI.transform.SetAsFirstSibling();
            toolUI.transform.DOMoveY(transform.position.y, _toolMovementDuration);
            yield return new WaitForSecondsRealtime(_toolMovementDuration);
            Destroy(toolUI.gameObject);
        }

        public void AnyToolEquipped(LevelEditorTool tool)
        {
            SetTool(tool);
            HideMenu();
        }

        private void SetTool(LevelEditorTool tool)
        {
            if (IsToolInPalette(tool))
            {
                _equippedTool = _tools.First(t => t.Tool == tool).Tool;
                _image.sprite = _equippedTool.ToolSprite;
            }
        }

        private bool IsToolInPalette(LevelEditorTool tool)
        {
            foreach (var toolUI in _tools)
            {
                if (toolUI.Tool == tool)
                    return true;
            }
            return false;
        }

        private void HideMenu()
        {
            for (int i = _tools.Count - 1; i >= 0; i--)
            {
                StartCoroutine(HideToolUI(_tools[i]));
            }
            _tools.Clear();
        }

        private bool MenuExists()
        {
            if (_tools.Count > 0)
                return true;
            return false;
        }
    }
}
