from selenium.webdriver import Firefox
from selenium.webdriver.common.by import By
import pandas as pd

driver = Firefox()
urls = ['https://zk.mk/teretani/skopje', 'https://zk.mk/teretani/skopje?skip=21','https://zk.mk/teretani/skopje?skip=41'
    ,'https://zk.mk/teretani/skopje?skip=61','https://zk.mk/teretani/skopje?skip=81']
df= pd.DataFrame(columns=['Name','Address', 'Number', 'WorkingTime','Areas'])

for X in range(0, len(urls)):
    driver.get(urls[X])

    names = driver.find_elements(By.CLASS_NAME, 'companyname')
    bigdetails = driver.find_elements(By.CLASS_NAME, 'detailresult')
    for j in range(0, len(bigdetails)):
        details = bigdetails[j].find_elements(By.CLASS_NAME, 'details')
        all_children_by_css = details[0].find_elements(By.CSS_SELECTOR, "*")
        areas = bigdetails[j].find_elements(By.CLASS_NAME, 'shortdescription')[1].get_attribute('textContent')
        name = bigdetails[j].find_elements(By.CLASS_NAME, 'companyname')[0].get_attribute('textContent')
        number = bigdetails[j].find_elements(By.CLASS_NAME, 'tcall')[0].get_attribute('textContent')
        working_time = ""
        adresa = ""
        for i in range(0, len(all_children_by_css)):
            if all_children_by_css[i].get_attribute('class') == 'h56146bf7196e9cd84da16cf2ccd2a8df':
                adresa = all_children_by_css[i].find_elements(By.CSS_SELECTOR, "*")[0].get_attribute('textContent')
            if all_children_by_css[i].get_attribute('class') == 'workingtime':
                working_time = all_children_by_css[i].get_attribute('textContent')
        df.loc[len(df.index)] = [str(name), str(adresa), str(number), str(working_time),
                                 str(areas.replace('Дејности:', ''))]

df.to_csv('data.csv', index=False)