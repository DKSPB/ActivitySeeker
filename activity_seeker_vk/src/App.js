import { useState, useEffect } from 'react';
import bridge from '@vkontakte/vk-bridge';
import { View, SplitLayout, SplitCol, ScreenSpinner } from '@vkontakte/vkui';
import { useActiveVkuiLocation } from '@vkontakte/vk-mini-apps-router';
import axios from 'axios';
import { Persik, Home } from './panels';
import { DEFAULT_VIEW_PANELS } from './routes';

export const App = () => {
  const { panel: activePanel = DEFAULT_VIEW_PANELS.HOME } = useActiveVkuiLocation();
  const [fetchedUser, setUser] = useState();
  const [popout, setPopout] = useState(<ScreenSpinner />);
  const [activityTypes, setActivityTypes] = useState([]);
  
  useEffect(() => {
    async function fetchData() {
      const user = await bridge.send('VKWebAppGetUserInfo');
      setUser(user);
      setPopout(null);
    }
    const getActivityTypes = async () => {
      try {
        const response = await axios.get('http://0.0.0.0:5199/api/activityType');
        setActivityTypes(response.data);
        console.log(response.data);
      } catch (error) {
        console.error('Ошибка при получении данных активности:', error);
      }
    };
    getActivityTypes();
    fetchData();
  }, []);

  return (
    <SplitLayout>
      <SplitCol>
        <View activePanel={activePanel}>
          <Home id="home" fetchedUser={fetchedUser} />
          <Persik id="persik" />
        </View>
      </SplitCol>
      {popout}
    </SplitLayout>
  );
};
