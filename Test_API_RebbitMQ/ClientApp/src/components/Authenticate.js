import React, { useEffect, useState } from 'react';
import { authenticationService } from '../services/AuthenticationService';
import { useHistory } from 'react-router-dom';
import { ButtonGroup, Button } from '@material-ui/core';
import TextField from '@material-ui/core/TextField';

export function Authenticate() {
  const [userName, setUserName] = useState('');
  const [password, setPassword] = useState('');

  let history = useHistory();

  useEffect(() => {
    if (authenticationService.currentUserValue) {
      history.push('/');
    }
  }, []);

  function onNameChange(e) {
    setUserName(e.target.value);
  }

  function onPasswordChange(e) {
    setPassword(e.target.value);
  }

  async function handleSubmit(event) {
    event.preventDefault();
    await authenticationService.login(userName, password);
    history.push('/OperationList');
  }

  return (
    <div>
      <h1>Login</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <TextField variant="outlined"
            placeholder="UserName"
            required
            onChange={onNameChange} />
        </div>
        <div>
          <TextField variant="outlined"
            placeholder="Password"
            required
            type='password'
            onChange={onPasswordChange} />
        </div>

        <ButtonGroup>
          <Button
            type="submit"
          >
            Login
                    </Button>
        </ButtonGroup>
      </form>
    </div>
  );

}
