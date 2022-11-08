#!/usr/bin/env python
# coding: utf-8

# In[1]:


import random 
import pandas as pd
import csv


# In[2]:


c = 20

Fathers = ['Mostafa', 'Ahmed', 'Mohamed', 'Eslam', 'Salah', 'Islam', 'Habib', 'Khalid',
        'Fathy', 'Hassan', 'Raffat', 'Moaz', 'Gaber', 'Shady', 'Hisham', 'Ali', 'Ehab']

Instructor = ['Mostafa', 'Ahmed', 'Mohamed', 'Eslam', 'Salah', 'Islam', 'Habib', 'Khalid',
              'Fathy', 'Hassan', 'Raffat', 'Moaz', 'Gaber', 'Shady', 'Hisham', 'Ali', 'Ehab',
              'Asmaa', 'Yara', 'Esraa', 'Mwada', 'Nermeen', 'Sondos',]


city = [ 'Cairo','Giza','Alexandria','Al Fayoum','Al Minya','Suez','Asyut','Sohag','Luxor',
        'Tanta','Ismailia','Aswan','Qena','Banha']


cols = ['Ins_id', 'Ins_Fname', 'Ins_Lname', 'Ins_Address', 'Ins_Age', 'Ins_Password', 'Ins_email','dept_id']


# In[5]:


# Boys dataset
df = pd.DataFrame(columns=cols)
df['Ins_Fname'] = random.choices(Instructor, k=c)
df.loc[:c, 'Ins_Lname'] = random.choices(Fathers, k=c)
df.loc[:c, 'Ins_Password'] = random.sample(range(10**1, 10**3), c)
df.loc[:c, 'Ins_Age'] = random.sample(range(27, 50), c)
df.loc[:c,'Ins_Address'] = random.choices(city, k=c)

for i in range(c):
    df.loc[i, 'Ins_email'] = df.Ins_Fname[i] + df.Ins_Lname[i] + str(random.randint(1900, 2300)) + '@gmail.com'


# In[6]:


df.to_csv("Instructor_table.csv",index=False)


# In[ ]:




