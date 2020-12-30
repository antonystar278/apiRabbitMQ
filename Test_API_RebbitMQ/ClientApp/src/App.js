import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Authenticate } from './components/Authenticate';
import { OperationList } from './components/OperationList';
import { NewOperation } from './components/NewOperation';
import { PrivateRoute } from './helpers/PrivateRoute';
import { StoreContext } from './store/StoreContext';
import { OperationStore } from './store/OperationStore';

import './custom.css'

const store = OperationStore();

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <StoreContext.Provider value={store}>
          <Route exact path='/' component={Authenticate} />
          <PrivateRoute path='/NewOperation' component={NewOperation} />
          <PrivateRoute path='/OperationList' component={OperationList} />
        </StoreContext.Provider>
      </Layout>
    );
  }
}
