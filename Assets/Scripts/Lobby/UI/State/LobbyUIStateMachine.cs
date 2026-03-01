using UnityEngine;
using System.Collections.Generic;

public class LobbyUIStateMachine
{
    private ILobbyUIState currentState;
    private Stack<ILobbyUIState> stateStack;

    private ILobbyUIState lobbyState;
    private ILobbyUIState characterShopState;
    private ILobbyUIState manageCharacterState;

    public ILobbyUIState LobbyState => lobbyState;
    public ILobbyUIState CharacterShopState => characterShopState;
    public ILobbyUIState ManageCharacterState => manageCharacterState;

    public LobbyUIStateMachine(LobbyUIManager lobbyUIManager)
    {
        lobbyState = lobbyUIManager.LowerUI;
        characterShopState = lobbyUIManager.CharacterShopUI;
        manageCharacterState = lobbyUIManager.ManagedCharacterUI;
    }

    public void Initialize(ILobbyUIState state)
    {
        stateStack = new Stack<ILobbyUIState>();

        currentState = state;
        state.ActiveUI();
    }

    public void ChangeState(ILobbyUIState state)
    {
        currentState.DeactiveUI();
        stateStack.Push(currentState);

        currentState = state;

        currentState.ActiveUI();

        if(currentState == lobbyState)
        {
            stateStack.Clear();
        }
    }

    public void UndoState()
    {
        if (currentState == lobbyState)
        {
            return;
        }

        currentState.DeactiveUI();

        currentState = stateStack.Pop();

        currentState.ActiveUI();
    }

    public void ResetState()
    {
        if (currentState == lobbyState)
        {
            return;
        }

        ChangeState(lobbyState);

        stateStack.Clear();
    }
}
