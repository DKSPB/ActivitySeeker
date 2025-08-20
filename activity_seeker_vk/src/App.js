import { useState, useEffect } from 'react';
import bridge from '@vkontakte/vk-bridge';
import { View, SplitLayout, ScreenSpinner, FormItem, Select, DateInput  } from '@vkontakte/vkui';
import { useActiveVkuiLocation } from '@vkontakte/vk-mini-apps-router';
import axios from 'axios';
import { Persik, Home } from './panels';
import { DEFAULT_VIEW_PANELS } from './routes';

export const App = () => {
  const { panel: activePanel = DEFAULT_VIEW_PANELS.HOME } = useActiveVkuiLocation();
  const [fetchedUser, setUser] = useState();
  const [popout, setPopout] = useState(<ScreenSpinner />);
  const [cities, setCities] = useState([]);
  const [activityTypes, setActivityTypes] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [selectedCity] = useState(null);
  useEffect(() => {
    bridge.send('VKWebAppInit');
    async function fetchUserData() {
      const user = await bridge.send('VKWebAppGetUserInfo');
      setUser(user);
      console.log(setUser)
      setPopout(null);
    }
    const fetchCities = async () => {
      try {
        const { access_token } = await bridge.send('VKWebAppGetAuthToken', {
        app_id: 53640516,
        scope: 'friends'
      });
        const response = await bridge.send('VKWebAppCallAPIMethod', {
          method: 'database.getCities',
          params: {
            access_token: String(access_token),
            country_id: 1, // ID России
            need_all: 1,
            count: 1000,
            offset: 0,
            v: '5.131'
          }
        });
        setCities(response.response.items);
        console.log(response.response.items);
        if (fetchedUser?.city?.id && cities.length > 0) {
          const match = cities.find(city => city.id === fetchedUser.city.id);
          if (match) {
            selectedCity(match.id);
          }
        }
      } catch (err) {
        setError(err);
        console.error('Ошибка получения городов:', err);
      }
    };
    const getActivityTypes = async () => {
      try {
        const response = await axios.get('http://localhost:5199/api/activityTypes?limit=20&offset=1');
        setActivityTypes(response.data);
        console.log(response.data);
      } catch (error) {
        console.error('Ошибка при получении данных активности:', error);
      }
    };
    fetchCities();
    getActivityTypes();
    fetchUserData();
    
  }, []);
  const cityOptions = cities.map(city => ({
    label: city.title,
    value: city.id
  }));
  return (
    <SplitLayout>
      <FormItem top="День проведения" htmlFor="date">
        <label htmlFor="select-id">Город</label>
        <Select
            id="select-id"
            placeholder="Не выбран"
            options={cityOptions}
            value={selectedCity}           
            onChange={e => setSelectedCity(Number(e.target.value))}
        />
        <DateInput id="date" aria-label="День проведения" />
      </FormItem>
      {popout}
    </SplitLayout>
  );
};
