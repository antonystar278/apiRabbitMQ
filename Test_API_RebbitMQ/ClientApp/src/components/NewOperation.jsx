import React, { useState } from 'react';
import { ButtonGroup, Button } from '@material-ui/core';
import TextField from '@material-ui/core/TextField';
import { useHistory } from 'react-router-dom';
import { OperationService } from '../services/OperationService';
import { authenticationService } from '../services/AuthenticationService';


export function NewOperation (){
  const [name, setName] = useState('');
  let history = useHistory();
  const userId = authenticationService.currentUserValue.data.id;
  const operationService = new OperationService();

  function onNameChange(e) {
    setName(e.target.value);
  }

  async function handleSubmit(event) {
    event.preventDefault();
    await operationService.createOperation(name, userId);
    history.push('/OperationList');
  }

  function toOperationList() {
    history.push('/OperationList');
  }

    return (
      <div>
        <h1>Create Operation</h1>

        <form onSubmit={handleSubmit}>
                <div>
                    <TextField variant="outlined"
                        placeholder="Operation Name"
                        required
                        onChange={onNameChange} />
                </div>

                <ButtonGroup>
                    <Button
                        type="submit"
                    >
                        Create
                    </Button>
                    <Button
                        onClick={toOperationList}
                    >
                        Cancel
                    </Button>
                </ButtonGroup>
        </form>
      </div>
    );
}
