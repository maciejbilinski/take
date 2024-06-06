import { createRouter, createWebHistory } from 'vue-router'
import ListView from '../views/ListView.vue'
import FormView from '../views/FormView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'list',
      component: ListView
    },
    {
      path: '/form',
      name: 'form',
      component: FormView
    }
  ]
})

export default router
